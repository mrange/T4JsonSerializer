open System.Buffers
open System.Text

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
  open System.Diagnostics
  open System.Text.Json
  open System.Text.Json.Serialization

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
            | JsonTokenType.PropertyName
            | JsonTokenType.String        ->
              r.GetString () |> ignore
            | JsonTokenType.Number        ->
              r.GetDouble () |> ignore
            | _                           ->
              ()

    let perf_Serialize_HardCoded inner =
      let enc           = UTF8Encoding false
      let utf8Id        = enc.GetBytes "Id"
      let utf8FirstName = enc.GetBytes "FirstName"
      let utf8LastName  = enc.GetBytes "LastName"
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
            w.WriteNumber (ReadOnlySpan<_> utf8Id       , p.Id        )
            w.WriteString (ReadOnlySpan<_> utf8FirstName, p.FirstName )
            w.WriteString (ReadOnlySpan<_> utf8LastName , p.LastName  )
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
        CsSourceGeneratorsJson.SourceGeneratorJsonSerializer.Serialize ps |> ignore

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
      if false then
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

    printfn "Perf test with total objects per run: %d" total
    for inner in inners do
      let outer = total / inner
      printfn "  outer: %d, inner: %d" outer inner
      for nm, tc in testCases do
        printfn "    Running test: '%s'" nm
        let _, ms, cc = time outer inner tc
        let secs      = decimal ms / 1000.0M
        printfn "    ... Took %.2f sec with collection count: %A" secs cc
    ()

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
