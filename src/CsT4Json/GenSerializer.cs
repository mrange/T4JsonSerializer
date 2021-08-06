#define UTF8_COMPARE




namespace CsT4Json
{
  using System;
  using System.Buffers;
  using System.Collections.Generic;
  using System.Text;
  using System.Text.Json;

  // --------------------------------------------------------------------------
  partial record Person
  {
    public int                  Id                             { get; set; }
    public string               FirstName                      { get; set; }
    public string               LastName                       { get; set; }
  }
  // --------------------------------------------------------------------------

  // --------------------------------------------------------------------------
  partial record Marriage
  {
    public Person               Husband                        { get; set; }
    public Person               Wife                           { get; set; }
    public double               MarriedFor                     { get; set; }
    public bool                 HappyCouple                    { get; set; }
  }
  // --------------------------------------------------------------------------

  // --------------------------------------------------------------------------
  partial record User
  {
    public int                  Id                             { get; set; }
    public string               FirstName                      { get; set; }
    public string               LastName                       { get; set; }
    public List<GeoLocation>    LastSeenAt                     { get; set; }
  }
  // --------------------------------------------------------------------------

  // --------------------------------------------------------------------------
  partial record GeoLocation
  {
    public double               Lo                             { get; set; }
    public double               La                             { get; set; }
    public string               TimeStamp                      { get; set; }
  }
  // --------------------------------------------------------------------------


  static partial class T4JsonSerializer
  {


    static readonly JsonEncodedText _enc_Person_Id                   = Encode("Id");
    static readonly JsonEncodedText _enc_Person_FirstName            = Encode("FirstName");
    static readonly JsonEncodedText _enc_Person_LastName             = Encode("LastName");

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out Person v)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        v = null;
        return r.Advance();
      }

      v = new Person();

      if (r.TokenType != JsonTokenType.StartObject)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType == JsonTokenType.PropertyName)
      {
#if UTF8_COMPARE
        if (r.ValueTextEquals(_enc_Person_Id.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out int vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.Id = vv;

          continue;
        }

        if (r.ValueTextEquals(_enc_Person_FirstName.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.FirstName = vv;

          continue;
        }

        if (r.ValueTextEquals(_enc_Person_LastName.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.LastName = vv;

          continue;
        }

#else
        var name = r.GetString();
        if (r.Advance() != DeserializeResult.Good)
        {
          return _bad;
        }

        switch (name)
        {
        case "Id":
          if (r.Deserialize(out int vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.Id = vv;
          break;

        case "FirstName":
          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.FirstName = vv;
          break;

        case "LastName":
          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.LastName = vv;
          break;

        default:
          return _bad;
        }
#endif
      }

      if (r.TokenType != JsonTokenType.EndObject)
      {
        return _bad;
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, Person v)
    {
      if (v is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartObject();

      w.WriteNumber(_enc_Person_Id, v.Id);

      w.WriteString(_enc_Person_FirstName, v.FirstName);

      w.WriteString(_enc_Person_LastName, v.LastName);


      w.WriteEndObject();
    }


    static readonly JsonEncodedText _enc_Marriage_Husband              = Encode("Husband");
    static readonly JsonEncodedText _enc_Marriage_Wife                 = Encode("Wife");
    static readonly JsonEncodedText _enc_Marriage_MarriedFor           = Encode("MarriedFor");
    static readonly JsonEncodedText _enc_Marriage_HappyCouple          = Encode("HappyCouple");

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out Marriage v)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        v = null;
        return r.Advance();
      }

      v = new Marriage();

      if (r.TokenType != JsonTokenType.StartObject)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType == JsonTokenType.PropertyName)
      {
#if UTF8_COMPARE
        if (r.ValueTextEquals(_enc_Marriage_Husband.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out Person vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.Husband = vv;

          continue;
        }

        if (r.ValueTextEquals(_enc_Marriage_Wife.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out Person vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.Wife = vv;

          continue;
        }

        if (r.ValueTextEquals(_enc_Marriage_MarriedFor.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out double vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.MarriedFor = vv;

          continue;
        }

        if (r.ValueTextEquals(_enc_Marriage_HappyCouple.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out bool vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.HappyCouple = vv;

          continue;
        }

#else
        var name = r.GetString();
        if (r.Advance() != DeserializeResult.Good)
        {
          return _bad;
        }

        switch (name)
        {
        case "Husband":
          if (r.Deserialize(out Person vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.Husband = vv;
          break;

        case "Wife":
          if (r.Deserialize(out Person vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.Wife = vv;
          break;

        case "MarriedFor":
          if (r.Deserialize(out double vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.MarriedFor = vv;
          break;

        case "HappyCouple":
          if (r.Deserialize(out bool vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.HappyCouple = vv;
          break;

        default:
          return _bad;
        }
#endif
      }

      if (r.TokenType != JsonTokenType.EndObject)
      {
        return _bad;
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, Marriage v)
    {
      if (v is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartObject();

      w.WritePropertyName(_enc_Marriage_Husband);
      w.Serialize(v.Husband);

      w.WritePropertyName(_enc_Marriage_Wife);
      w.Serialize(v.Wife);

      w.WriteNumber(_enc_Marriage_MarriedFor, v.MarriedFor);

      w.WriteBoolean(_enc_Marriage_HappyCouple, v.HappyCouple);


      w.WriteEndObject();
    }


    static readonly JsonEncodedText _enc_User_Id                   = Encode("Id");
    static readonly JsonEncodedText _enc_User_FirstName            = Encode("FirstName");
    static readonly JsonEncodedText _enc_User_LastName             = Encode("LastName");
    static readonly JsonEncodedText _enc_User_LastSeenAt           = Encode("LastSeenAt");

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out User v)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        v = null;
        return r.Advance();
      }

      v = new User();

      if (r.TokenType != JsonTokenType.StartObject)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType == JsonTokenType.PropertyName)
      {
#if UTF8_COMPARE
        if (r.ValueTextEquals(_enc_User_Id.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out int vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.Id = vv;

          continue;
        }

        if (r.ValueTextEquals(_enc_User_FirstName.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.FirstName = vv;

          continue;
        }

        if (r.ValueTextEquals(_enc_User_LastName.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.LastName = vv;

          continue;
        }

        if (r.ValueTextEquals(_enc_User_LastSeenAt.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out List<GeoLocation> vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.LastSeenAt = vv;

          continue;
        }

#else
        var name = r.GetString();
        if (r.Advance() != DeserializeResult.Good)
        {
          return _bad;
        }

        switch (name)
        {
        case "Id":
          if (r.Deserialize(out int vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.Id = vv;
          break;

        case "FirstName":
          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.FirstName = vv;
          break;

        case "LastName":
          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.LastName = vv;
          break;

        case "LastSeenAt":
          if (r.Deserialize(out List<GeoLocation> vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.LastSeenAt = vv;
          break;

        default:
          return _bad;
        }
#endif
      }

      if (r.TokenType != JsonTokenType.EndObject)
      {
        return _bad;
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, User v)
    {
      if (v is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartObject();

      w.WriteNumber(_enc_User_Id, v.Id);

      w.WriteString(_enc_User_FirstName, v.FirstName);

      w.WriteString(_enc_User_LastName, v.LastName);

      w.WritePropertyName(_enc_User_LastSeenAt);
      w.Serialize(v.LastSeenAt);


      w.WriteEndObject();
    }


    static readonly JsonEncodedText _enc_GeoLocation_Lo                   = Encode("Lo");
    static readonly JsonEncodedText _enc_GeoLocation_La                   = Encode("La");
    static readonly JsonEncodedText _enc_GeoLocation_TimeStamp            = Encode("TimeStamp");

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out GeoLocation v)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        v = null;
        return r.Advance();
      }

      v = new GeoLocation();

      if (r.TokenType != JsonTokenType.StartObject)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType == JsonTokenType.PropertyName)
      {
#if UTF8_COMPARE
        if (r.ValueTextEquals(_enc_GeoLocation_Lo.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out double vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.Lo = vv;

          continue;
        }

        if (r.ValueTextEquals(_enc_GeoLocation_La.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out double vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.La = vv;

          continue;
        }

        if (r.ValueTextEquals(_enc_GeoLocation_TimeStamp.EncodedUtf8Bytes))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.TimeStamp = vv;

          continue;
        }

#else
        var name = r.GetString();
        if (r.Advance() != DeserializeResult.Good)
        {
          return _bad;
        }

        switch (name)
        {
        case "Lo":
          if (r.Deserialize(out double vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.Lo = vv;
          break;

        case "La":
          if (r.Deserialize(out double vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.La = vv;
          break;

        case "TimeStamp":
          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.TimeStamp = vv;
          break;

        default:
          return _bad;
        }
#endif
      }

      if (r.TokenType != JsonTokenType.EndObject)
      {
        return _bad;
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, GeoLocation v)
    {
      if (v is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartObject();

      w.WriteNumber(_enc_GeoLocation_Lo, v.Lo);

      w.WriteNumber(_enc_GeoLocation_La, v.La);

      w.WriteString(_enc_GeoLocation_TimeStamp, v.TimeStamp);


      w.WriteEndObject();
    }


    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out List<Person> vs)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        vs = null;
        return r.Advance();
      }

      vs = new List<Person>(16);

      if (r.TokenType != JsonTokenType.StartArray)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType != JsonTokenType.EndArray)
      {
        if (r.Deserialize(out Person vv) != DeserializeResult.Good)
        {
          return _bad;
        }

        vs.Add(vv);
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, List<Person> vs)
    {
      if (vs is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartArray();

      unchecked
      {
        var c = vs.Count;
        for(int i = 0; i < c; ++i)
        {
          w.Serialize(vs[i]);
        }
      }

      w.WriteEndArray();
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out List<Marriage> vs)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        vs = null;
        return r.Advance();
      }

      vs = new List<Marriage>(16);

      if (r.TokenType != JsonTokenType.StartArray)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType != JsonTokenType.EndArray)
      {
        if (r.Deserialize(out Marriage vv) != DeserializeResult.Good)
        {
          return _bad;
        }

        vs.Add(vv);
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, List<Marriage> vs)
    {
      if (vs is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartArray();

      unchecked
      {
        var c = vs.Count;
        for(int i = 0; i < c; ++i)
        {
          w.Serialize(vs[i]);
        }
      }

      w.WriteEndArray();
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out List<User> vs)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        vs = null;
        return r.Advance();
      }

      vs = new List<User>(16);

      if (r.TokenType != JsonTokenType.StartArray)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType != JsonTokenType.EndArray)
      {
        if (r.Deserialize(out User vv) != DeserializeResult.Good)
        {
          return _bad;
        }

        vs.Add(vv);
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, List<User> vs)
    {
      if (vs is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartArray();

      unchecked
      {
        var c = vs.Count;
        for(int i = 0; i < c; ++i)
        {
          w.Serialize(vs[i]);
        }
      }

      w.WriteEndArray();
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out List<GeoLocation> vs)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        vs = null;
        return r.Advance();
      }

      vs = new List<GeoLocation>(16);

      if (r.TokenType != JsonTokenType.StartArray)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType != JsonTokenType.EndArray)
      {
        if (r.Deserialize(out GeoLocation vv) != DeserializeResult.Good)
        {
          return _bad;
        }

        vs.Add(vv);
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, List<GeoLocation> vs)
    {
      if (vs is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartArray();

      unchecked
      {
        var c = vs.Count;
        for(int i = 0; i < c; ++i)
        {
          w.Serialize(vs[i]);
        }
      }

      w.WriteEndArray();
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out List<bool> vs)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        vs = null;
        return r.Advance();
      }

      vs = new List<bool>(16);

      if (r.TokenType != JsonTokenType.StartArray)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType != JsonTokenType.EndArray)
      {
        if (r.Deserialize(out bool vv) != DeserializeResult.Good)
        {
          return _bad;
        }

        vs.Add(vv);
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, List<bool> vs)
    {
      if (vs is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartArray();

      unchecked
      {
        var c = vs.Count;
        for(int i = 0; i < c; ++i)
        {
          w.Serialize(vs[i]);
        }
      }

      w.WriteEndArray();
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out List<double> vs)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        vs = null;
        return r.Advance();
      }

      vs = new List<double>(16);

      if (r.TokenType != JsonTokenType.StartArray)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType != JsonTokenType.EndArray)
      {
        if (r.Deserialize(out double vv) != DeserializeResult.Good)
        {
          return _bad;
        }

        vs.Add(vv);
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, List<double> vs)
    {
      if (vs is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartArray();

      unchecked
      {
        var c = vs.Count;
        for(int i = 0; i < c; ++i)
        {
          w.Serialize(vs[i]);
        }
      }

      w.WriteEndArray();
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out List<string> vs)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        vs = null;
        return r.Advance();
      }

      vs = new List<string>(16);

      if (r.TokenType != JsonTokenType.StartArray)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType != JsonTokenType.EndArray)
      {
        if (r.Deserialize(out string vv) != DeserializeResult.Good)
        {
          return _bad;
        }

        vs.Add(vv);
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, List<string> vs)
    {
      if (vs is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartArray();

      unchecked
      {
        var c = vs.Count;
        for(int i = 0; i < c; ++i)
        {
          w.Serialize(vs[i]);
        }
      }

      w.WriteEndArray();
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out List<int> vs)
    {
      if (r.TokenType == JsonTokenType.Null)
      {
        vs = null;
        return r.Advance();
      }

      vs = new List<int>(16);

      if (r.TokenType != JsonTokenType.StartArray)
      {
        return _bad;
      }

      if (r.Advance() != DeserializeResult.Good)
      {
        return _bad;
      }

      while(r.TokenType != JsonTokenType.EndArray)
      {
        if (r.Deserialize(out int vv) != DeserializeResult.Good)
        {
          return _bad;
        }

        vs.Add(vv);
      }

      return r.Advance();
    }

    public static void Serialize(this Utf8JsonWriter w, List<int> vs)
    {
      if (vs is null)
      {
        w.WriteNullValue();
        return;
      }

      w.WriteStartArray();

      unchecked
      {
        var c = vs.Count;
        for(int i = 0; i < c; ++i)
        {
          w.Serialize(vs[i]);
        }
      }

      w.WriteEndArray();
    }


    public static void Deserialize(this byte[] bs, out Person v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this Person v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out Marriage v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this Marriage v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out User v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this User v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out GeoLocation v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this GeoLocation v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out bool v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this bool v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out double v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this double v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out string v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this string v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out int v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this int v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out List<Person> v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this List<Person> v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out List<Marriage> v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this List<Marriage> v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out List<User> v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this List<User> v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out List<GeoLocation> v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this List<GeoLocation> v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out List<bool> v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this List<bool> v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out List<double> v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this List<double> v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out List<string> v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this List<string> v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }

    public static void Deserialize(this byte[] bs, out List<int> v)
    {
      var inp = new ReadOnlySpan<byte>(bs);
//      var inp = new ReadOnlySequence<byte>(bs);
      var state = new JsonReaderState(_opts);
      var r = new Utf8JsonReader(inp, true, state);
      if (!r.Read())
      {
        r.RaiseInvalidInput();
      }

      var rr = r.Deserialize(out v);
      switch(rr)
      {
      case DeserializeResult.Good:
        r.RaiseInvalidInput();
        break;
      case DeserializeResult.GoodEOS:
        break;
      case DeserializeResult.Bad:
        r.RaiseInvalidInput();
        break;
      }
    }

    public static byte[] Serialize(this List<int> v, bool indented = false)
    {
      var opts = new JsonWriterOptions()
      {
        Indented        = indented  ,
        SkipValidation  = true      ,
      };

      var mb = _tlsBuffer.Value;
      mb.Clear ();

      using(var w = new Utf8JsonWriter(mb, opts))
      {
        w.Serialize(v);
      }

      return mb.WrittenSpan.ToArray();
    }


  }
}
