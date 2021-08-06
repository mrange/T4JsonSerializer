module FunctionalTests =
  open System

  open FsCheck

  open CsT4Json

  let fixPersons (ps : ResizeArray<Person>) =
    ps

  let fixMarriages (ps : ResizeArray<Marriage>) =
    let mapper (p : Marriage) =
      let marriedFor =
        if    Double.IsNaN      p.MarriedFor then 0.0
        elif  Double.IsInfinity p.MarriedFor then 0.0
        else p.MarriedFor
      Marriage (
          Husband     = p.Husband
        , Wife        = p.Wife
        , MarriedFor  = marriedFor
        , HappyCouple = p.HappyCouple
        )
    ps
    |> Seq.map mapper
    |> ResizeArray<_>
  type Properties =
    class
      static member ``Person serialization round-trip`` (indented : bool) (ps : ResizeArray<Person>) =
        let ps  = fixPersons ps
        let e   = ps.ToArray ()
        let bs  = ps.Serialize indented
        let mutable a = ResizeArray<Person> ()
        bs.Deserialize &a
        let a = a.ToArray ()
        e = a

      static member ``Marriage serialization round-trip`` (indented : bool) (ps : ResizeArray<Marriage>) =
        let ps  = fixMarriages ps
        let e   = ps.ToArray ()
        let bs  = ps.Serialize indented
        let mutable a = ResizeArray<Marriage> ()
        bs.Deserialize &a
        let a = a.ToArray ()
        e = a

    end
  let run () =
#if DEBUG
    let config = Config.Default
#else
    let config = { Config.Default with MaxTest = 1000 }
#endif

    Check.All<Properties> config


module PerformanceTests =
  open System
  open System.Buffers
  open System.Diagnostics
  open System.Globalization
  open System.IO
  open System.Text
  open System.Text.Json
  open System.Text.Json.Serialization

  open Plotly.NET

  open CsT4Json

  let stopWatch =
    let sw = new Stopwatch ()
    sw.Start ()
    sw
  let now () = stopWatch.ElapsedMilliseconds

  let time outer inner f =
    let f = f inner
    let v = f ()
    let inline cc i = GC.CollectionCount i
    GC.Collect (2, GCCollectionMode.Forced, true)
    let bcc0, bcc1, bcc2 = cc 0, cc 1, cc 2
    let before = now ()
    for i = 1 to outer do
      f () |> ignore
    let after = now ()
    let acc0, acc1, acc2 = cc 0, cc 1, cc 2
    v, after - before, (acc0 - bcc0, acc1 - bcc1, acc2 - bcc2)

  let writeChart fileName (dataPoints : Map<string*string, decimal>) =
    printfn "Writing chart: %s" fileName

    let allRows =
      dataPoints
      |> Seq.map (fun kv -> fst kv.Key)
      |> Seq.distinct
      |> Seq.sort
      |> Seq.toList
    let allColumns =
      dataPoints
      |> Seq.map (fun kv -> snd kv.Key)
      |> Seq.distinct
      |> Seq.sort
      |> Seq.toList

    let charts =
      let mapper row =
        let values = 
          let mapper col = 
            match dataPoints |> Map.tryFind (row, col) with
            | None    -> 0.M
            | Some dp -> dp
          allColumns |> List.map mapper
        Chart.Column(allColumns, values, Name = row , Showlegend = true)
      allRows |> List.map mapper

    Chart.Combine charts
    |> Chart.SaveHtmlAs fileName

  let writeCSV (name  : string) (col0 : string) (dataPoints : Map<string*string, decimal>) =
    let name = name + ".csv"
    printfn "Writing %s" name
    let allRows =
      dataPoints
      |> Seq.map (fun kv -> fst kv.Key)
      |> Seq.distinct
      |> Seq.sort
      |> Seq.toArray
    let allColumns =
      dataPoints
      |> Seq.map (fun kv -> snd kv.Key)
      |> Seq.distinct
      |> Seq.sort
      |> Seq.toArray

    use sw = new StreamWriter (name : string)
    sw.Write (col0 : string)
    for col in allColumns do
      sw.Write ", "
      sw.Write col
    sw.WriteLine ()

    for row in allRows do
      sw.Write row
      for col in allColumns do
        sw.Write ", "
        match dataPoints |> Map.tryFind (row, col) with
        | None    -> ()
        | Some dp -> sw.Write (string dp)
      sw.WriteLine ()

  let run () =
    let total   = 1_000_000
    let inners  = [|10; 100; 1000|]
    let bytes   =
      let opt = JsonSerializerOptions (WriteIndented = false)
      let person i = Person(Id = i, FirstName = "Hello", LastName = "There")
      let mapper inner =
        let ps = Array.init inner person
        let bs = JsonSerializer.SerializeToUtf8Bytes (ps, opt)
        inner, (ResizeArray<_> ps, bs)
      inners |> Array.map mapper |> Map.ofArray

    let nops  = ResizeArray<Person> ()

    let perf_Deserialize_Consume inner =
      let opt = JsonReaderOptions (CommentHandling = JsonCommentHandling.Skip)
      let bs  = snd bytes.[inner]
      fun () ->
        let inp = ReadOnlySpan<_> bs
        let r = new Utf8JsonReader(inp, opt)
        while r.Read () do
          match r.TokenType with
            // Parse these tokens as this is done when extracting values
            | JsonTokenType.String        ->
              r.GetString () |> ignore
            | JsonTokenType.Number        ->
              r.GetDouble () |> ignore
            | _                           ->
              ()

    let perf_Serialize_HardCoded inner =
      let enc id        = JsonEncodedText.Encode ((id : string), null)
      let encId         = enc "Id"
      let encFirstName  = enc "FirstName"
      let encLastName   = enc "LastName"
      let ps            = fst bytes.[inner]
      let opts          = JsonWriterOptions(Indented = false, SkipValidation = true)
      let buf           = ArrayBufferWriter<byte> (256)

      fun () ->
        do
          buf.Clear ()
          use w = new Utf8JsonWriter(buf, opts)
          w.WriteStartArray ()
          let c = ps.Count
          for i = 0 to c - 1 do
            let p = ps.[i]
            w.WriteStartObject ()
            w.WriteNumber (encId       , p.Id        )
            w.WriteString (encFirstName, p.FirstName )
            w.WriteString (encLastName , p.LastName  )
            w.WriteEndObject ()
          w.WriteEndArray ()
        buf.WrittenSpan.ToArray () |> ignore
        ()

    let perf_Deserialize_JsonSerializer inner =
      let opt = JsonSerializerOptions (ReadCommentHandling = JsonCommentHandling.Skip)
      let bs  = snd bytes.[inner]
      fun () ->
        let inp = ReadOnlySpan<_> bs
        JsonSerializer.Deserialize<ResizeArray<Person>>(inp, opt) |> ignore

    let perf_Serialize_JsonSerializer inner =
      let opt = JsonSerializerOptions (WriteIndented = false)
      let ps  = fst bytes.[inner]
      fun () ->
        JsonSerializer.SerializeToUtf8Bytes(ps, opt) |> ignore

    let perf_Deserialize_SourceGenerator inner =
      let bs  = snd bytes.[inner]
      fun () ->
        CsSourceGeneratorsJson.SourceGeneratorJsonSerializer.Deserialize bs |> ignore

    let perf_Serialize_SourceGenerator inner =
      let mapper (p : Person) =
        CsSourceGeneratorsJson.Person(Id = p.Id, FirstName = p.FirstName, LastName = p.LastName)
      let ps  = fst bytes.[inner]
      let ps  = ps |> Seq.map mapper |> ResizeArray<_>
      fun () ->
        CsSourceGeneratorsJson.SourceGeneratorJsonSerializer.Serialize (ps, false) |> ignore

    let perf_Deserialize_T4JsonSerializer inner =
      let bs  = snd bytes.[inner]
      fun () ->
        let mutable ps = nops
        bs.Deserialize &ps

    let perf_Serialize_T4JsonSerializer inner =
      let ps  = fst bytes.[inner]
      fun () ->
        ps.Serialize(false) |> ignore

    let testCases =
      let experimenting = false
      if experimenting then
        [|
          "Deserialize.Consume"         , perf_Deserialize_Consume
          "Deserialize.SourceGenerator" , perf_Deserialize_SourceGenerator
          "Serialize.HardCoded"         , perf_Serialize_HardCoded
          "Serialize.SourceGenerator"   , perf_Serialize_SourceGenerator
        |]
      else
        [|
          "Deserialize.Consume"         , perf_Deserialize_Consume
          "Deserialize.JsonSerializer"  , perf_Deserialize_JsonSerializer
          "Deserialize.T4JsonSerializer", perf_Deserialize_T4JsonSerializer
          "Deserialize.SourceGenerator" , perf_Deserialize_SourceGenerator
          "Serialize.HardCoded"         , perf_Serialize_HardCoded
          "Serialize.JsonSerializer"    , perf_Serialize_JsonSerializer
          "Serialize.T4JsonSerializer"  , perf_Serialize_T4JsonSerializer
          "Serialize.SourceGenerator"   , perf_Serialize_SourceGenerator
        |]

    let mutable times     = Map.empty
    let mutable collects  = Map.empty

    printfn "Warm up"
    for nm, tc in testCases do
      printfn "    Warming up: '%s'" nm
      let f = tc 100
      for i = 0 to 100 do
        f ()

    printfn "Perf test with total objects per run: %d" total
    for inner in inners do
      let outer = total / inner
      printfn "  outer: %d, inner: %d" outer inner
      for nm, tc in testCases do
        printfn "    Running test: '%s'" nm
        let _, ms, cc   = time outer inner tc
        let secs        = decimal ms / 1000.0M
        let cc0, _, _0  = cc
        times     <- times    |> Map.add (string inner, nm) secs
        collects  <- collects |> Map.add (string inner, nm) (decimal cc0)
        printfn "    ... Took %.2f sec with collection count: %A" secs cc

    writeChart "times"      times
    writeChart "collects"   collects

    writeCSV "times"        "Inner" times
    writeCSV "collects"     "Inner" collects

open System.Globalization

[<EntryPoint>]
let main argv =
  CultureInfo.CurrentCulture <- CultureInfo.InvariantCulture
  FunctionalTests.run ()
#if DEBUG
#else
  PerformanceTests.run ()
#endif
  0
