﻿#define UTF8_COMPARE


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
    public string               Married                        { get; set; }
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
    static readonly Encoding _enc = new UTF8Encoding(false);


    static readonly byte[] _utf8_Person_Id                   = _enc.GetBytes("Id");
    static readonly byte[] _utf8_Person_FirstName            = _enc.GetBytes("FirstName");
    static readonly byte[] _utf8_Person_LastName             = _enc.GetBytes("LastName");

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
        if (r.ValueTextEquals(_utf8_Person_Id))
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

        if (r.ValueTextEquals(_utf8_Person_FirstName))
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

        if (r.ValueTextEquals(_utf8_Person_LastName))
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

      w.WritePropertyName(_utf8_Person_Id);
      w.Serialize(v.Id);

      w.WritePropertyName(_utf8_Person_FirstName);
      w.Serialize(v.FirstName);

      w.WritePropertyName(_utf8_Person_LastName);
      w.Serialize(v.LastName);


      w.WriteEndObject();
    }


    static readonly byte[] _utf8_Marriage_Husband              = _enc.GetBytes("Husband");
    static readonly byte[] _utf8_Marriage_Wife                 = _enc.GetBytes("Wife");
    static readonly byte[] _utf8_Marriage_Married              = _enc.GetBytes("Married");

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
        if (r.ValueTextEquals(_utf8_Marriage_Husband))
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

        if (r.ValueTextEquals(_utf8_Marriage_Wife))
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

        if (r.ValueTextEquals(_utf8_Marriage_Married))
        {
          if (r.Advance() != DeserializeResult.Good)
          {
            return _bad;
          }

          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }

          v.Married = vv;

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

        case "Married":
          if (r.Deserialize(out string vv) != DeserializeResult.Good)
          {
            return _bad;
          }
          v.Married = vv;
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

      w.WritePropertyName(_utf8_Marriage_Husband);
      w.Serialize(v.Husband);

      w.WritePropertyName(_utf8_Marriage_Wife);
      w.Serialize(v.Wife);

      w.WritePropertyName(_utf8_Marriage_Married);
      w.Serialize(v.Married);


      w.WriteEndObject();
    }


    static readonly byte[] _utf8_User_Id                   = _enc.GetBytes("Id");
    static readonly byte[] _utf8_User_FirstName            = _enc.GetBytes("FirstName");
    static readonly byte[] _utf8_User_LastName             = _enc.GetBytes("LastName");
    static readonly byte[] _utf8_User_LastSeenAt           = _enc.GetBytes("LastSeenAt");

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
        if (r.ValueTextEquals(_utf8_User_Id))
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

        if (r.ValueTextEquals(_utf8_User_FirstName))
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

        if (r.ValueTextEquals(_utf8_User_LastName))
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

        if (r.ValueTextEquals(_utf8_User_LastSeenAt))
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

      w.WritePropertyName(_utf8_User_Id);
      w.Serialize(v.Id);

      w.WritePropertyName(_utf8_User_FirstName);
      w.Serialize(v.FirstName);

      w.WritePropertyName(_utf8_User_LastName);
      w.Serialize(v.LastName);

      w.WritePropertyName(_utf8_User_LastSeenAt);
      w.Serialize(v.LastSeenAt);


      w.WriteEndObject();
    }


    static readonly byte[] _utf8_GeoLocation_Lo                   = _enc.GetBytes("Lo");
    static readonly byte[] _utf8_GeoLocation_La                   = _enc.GetBytes("La");
    static readonly byte[] _utf8_GeoLocation_TimeStamp            = _enc.GetBytes("TimeStamp");

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
        if (r.ValueTextEquals(_utf8_GeoLocation_Lo))
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

        if (r.ValueTextEquals(_utf8_GeoLocation_La))
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

        if (r.ValueTextEquals(_utf8_GeoLocation_TimeStamp))
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

      w.WritePropertyName(_utf8_GeoLocation_Lo);
      w.Serialize(v.Lo);

      w.WritePropertyName(_utf8_GeoLocation_La);
      w.Serialize(v.La);

      w.WritePropertyName(_utf8_GeoLocation_TimeStamp);
      w.Serialize(v.TimeStamp);


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

      var c = vs.Count;
      for(int i = 0; i < c; ++i)
      {
        w.Serialize(vs[i]);
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

      var c = vs.Count;
      for(int i = 0; i < c; ++i)
      {
        w.Serialize(vs[i]);
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

      var c = vs.Count;
      for(int i = 0; i < c; ++i)
      {
        w.Serialize(vs[i]);
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

      var c = vs.Count;
      for(int i = 0; i < c; ++i)
      {
        w.Serialize(vs[i]);
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

      var c = vs.Count;
      for(int i = 0; i < c; ++i)
      {
        w.Serialize(vs[i]);
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

      var c = vs.Count;
      for(int i = 0; i < c; ++i)
      {
        w.Serialize(vs[i]);
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

      var c = vs.Count;
      for(int i = 0; i < c; ++i)
      {
        w.Serialize(vs[i]);
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

      var c = vs.Count;
      for(int i = 0; i < c; ++i)
      {
        w.Serialize(vs[i]);
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
