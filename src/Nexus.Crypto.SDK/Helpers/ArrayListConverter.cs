﻿using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexus.Crypto.SDK.Helpers;

public class ArrayListConverter<TItem> : JsonConverter
{
    public override bool CanWrite { get { return false; } }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        var list = serializer.Deserialize<List<TItem>>(reader);

        var arrayList = existingValue as ArrayList ?? new ArrayList(list.Count);
        arrayList.AddRange(list);

        return arrayList;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(ArrayList);
    }
}
