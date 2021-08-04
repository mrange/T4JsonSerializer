namespace CsSourceGeneratorsJson
{
  using System;
  using System.Collections.Generic;
  using System.Text.Json.Serialization;
  using System.Text.Json;
  using System.Linq;
using System.Buffers;
using System.Threading;

// --------------------------------------------------------------------------
  public partial record Person
  {
    public int                  Id                             { get; set; }
    public string               FirstName                      { get; set; }
    public string               LastName                       { get; set; }
  }
  // --------------------------------------------------------------------------
  [JsonSerializable(typeof(Person)      , GenerationMode = default)]
  [JsonSerializable(typeof(List<Person>), GenerationMode = default)]
  partial class MyJsonContext : JsonSerializerContext
  {
  }

  public static class SourceGeneratorJsonSerializer
  {
    static readonly JsonSerializerOptions _opt = new JsonSerializerOptions ()
    {
      ReadCommentHandling = JsonCommentHandling.Skip  ,
      WriteIndented       = false                     ,
    };

    static readonly ThreadLocal<ArrayBufferWriter<byte>> _tlsBuffer =
      new ThreadLocal<ArrayBufferWriter<byte>>(() => new ArrayBufferWriter<byte>(256), false);

    public static byte[] Serialize (List<Person> ps, bool indented)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var buf = _tlsBuffer.Value;
      buf.Clear ();

      using (var w = new Utf8JsonWriter(buf, opts))
      {
        MyJsonContext.Default.ListPerson.Serialize(w, ps);
      }
      
      return buf.WrittenSpan.ToArray();
    }

    public static List<Person> Deserialize (byte[] bs) =>
      JsonSerializer.Deserialize(bs, MyJsonContext.Default.ListPerson)
      ;
  }
}
