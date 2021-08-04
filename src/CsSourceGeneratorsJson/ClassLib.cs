namespace CsSourceGeneratorsJson
{
  using System;
  using System.Collections.Generic;
  using System.Text.Json.Serialization;
  using System.Text.Json;
  using System.Linq;

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
    public static byte[] Serialize (List<Person> ps) =>
      JsonSerializer.SerializeToUtf8Bytes(ps, MyJsonContext.Default.ListPerson)
      ;
    public static List<Person> Deserialize (byte[] bs) =>
      JsonSerializer.Deserialize(bs, MyJsonContext.Default.ListPerson)
      ;
  }
}
