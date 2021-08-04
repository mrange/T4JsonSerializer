namespace CsT4Json
{
  using System;
  using System.Buffers;
  using System.Runtime.CompilerServices;
  using System.Text;
  using System.Text.Json;
  using System.Threading;

  public enum DeserializeResult
  {
    Good    = 0 ,
    GoodEOS = 1 ,
    Bad     = -1,
  }

  static partial class T4JsonSerializer
  {
    static readonly DeserializeResult _bad = DeserializeResult.Bad;

    static readonly JsonReaderOptions _opts = new JsonReaderOptions()
    {
        CommentHandling = JsonCommentHandling.Skip
    };

    static readonly ThreadLocal<ArrayBufferWriter<byte>> _tlsBuffer =
      new ThreadLocal<ArrayBufferWriter<byte>> (() => new ArrayBufferWriter<byte>(256), false);

    [MethodImpl(MethodImplOptions.AggressiveInlining|MethodImplOptions.AggressiveOptimization)]
    static DeserializeResult Advance(this ref Utf8JsonReader r)
    {
      if (!r.Read())
      {
        return DeserializeResult.GoodEOS;
      }

#if SKIP_COMMENTS
      return r.TokenType switch
      {
        // Skip these, not sure about None
        JsonTokenType.None    => r.Advance()
      , JsonTokenType.Comment => r.Advance()
      , _                     => DeserializeResult.Good
      };
#else
      return DeserializeResult.Good;
#endif
    }

    public static void RaiseInvalidInput(this ref Utf8JsonReader r)
    {
      throw new Exception($"Invalid input @ {r.BytesConsumed}");
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out double v)
    {
      v = default;
      switch(r.TokenType)
      {
      case JsonTokenType.Number:
        v = r.GetDouble();
        return r.Advance();
      default:
        return _bad;
      }
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out int v)
    {
      v = default;
      switch(r.TokenType)
      {
      case JsonTokenType.Number:
        v = r.GetInt32();
        return r.Advance();
      default:
        return _bad;
      }
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out string v)
    {
      v = default;
      switch(r.TokenType)
      {
      case JsonTokenType.String:
        v = r.GetString();
        return r.Advance();
      case JsonTokenType.Null:
        v = null;
        return r.Advance();
      default:
        return _bad;
      }
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out bool v)
    {
      v = default;
      switch(r.TokenType)
      {
      case JsonTokenType.True:
        v = true;
        return r.Advance();
      case JsonTokenType.False:
        v = false;
        return r.Advance();
      default:
        return _bad;
      }
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out double? v)
    {
      v = default;
      switch(r.TokenType)
      {
      case JsonTokenType.Number:
        v = r.GetDouble();
        return r.Advance();
      case JsonTokenType.Null:
        v = null;
        return r.Advance();
      default:
        return _bad;
      }
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out int? v)
    {
      v = default;
      switch(r.TokenType)
      {
      case JsonTokenType.Number:
        v = r.GetInt32();
        return r.Advance();
      case JsonTokenType.Null:
        v = null;
        return r.Advance();
      default:
        return _bad;
      }
    }

    public static DeserializeResult Deserialize(this ref Utf8JsonReader r, out bool? v)
    {
      v = default;
      switch(r.TokenType)
      {
      case JsonTokenType.True:
        v = true;
        return r.Advance();
      case JsonTokenType.False:
        v = false;
        return r.Advance();
      case JsonTokenType.Null:
        v = null;
        return r.Advance();
      default:
        return _bad;
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Serialize(this Utf8JsonWriter w, double v) =>
      w.WriteNumberValue(v)
      ;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Serialize(this Utf8JsonWriter w, double? v)
    {
      if (v is not null)
      {
        w.WriteNumberValue(v.Value);
      }
      else
      {
        w.WriteNullValue();
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Serialize(this Utf8JsonWriter w, int v) =>
      w.WriteNumberValue(v)
      ;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Serialize(this Utf8JsonWriter w, int? v)
    {
      if (v is not null)
      {
        w.WriteNumberValue(v.Value);
      }
      else
      {
        w.WriteNullValue();
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Serialize(this Utf8JsonWriter w, bool v) =>
      w.WriteBooleanValue(v)
      ;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Serialize(this Utf8JsonWriter w, bool? v)
    {
      if (v is not null)
      {
        w.WriteBooleanValue(v.Value);
      }
      else
      {
        w.WriteNullValue();
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Serialize(this Utf8JsonWriter w, string v)
    {
      if (v is not null)
      {
        w.WriteStringValue(v);
      }
      else
      {
        w.WriteNullValue();
      }
    }
  }
}