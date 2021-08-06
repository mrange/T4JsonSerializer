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
  public static class SourceGeneratorJsonSerializer
  {
    public static byte[] Serialize (List<Person> ps, bool indented)
    {
      var opts = new JsonSerializerOptions()
      {
        WriteIndented       = indented                  ,
      };

      return JsonSerializer.SerializeToUtf8Bytes(ps, opts);
    }

    public static List<Person> Deserialize (byte[] bs)
    {
      var opts = new JsonSerializerOptions()
      {
        ReadCommentHandling = JsonCommentHandling.Skip  ,
      };

      return JsonSerializer.Deserialize<List<Person>>(bs);
    }
  }
}
