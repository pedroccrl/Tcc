﻿using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Converters
{
    /*
     * BSON ObjectID is a 12-byte value consisting of:
     * - a 4-byte timestamp (seconds since epoch)
     * - a 3-byte machine id
     * - a 2-byte process id
     * - a 3-byte counter
     * 
     * 0123 456     78  91011
     * time machine pid inc
     */
    public class ObjectIdConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ObjectId);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                throw new Exception(
                    string.Format("Unexpected token parsing ObjectId. Expected String, got {0}.",
                        reader.TokenType));
            }

            var value = (string)reader.Value;
            return string.IsNullOrEmpty(value) ? ObjectId.Empty : new ObjectId(value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is ObjectId objectId)
            {
                writer.WriteValue(objectId != ObjectId.Empty ? objectId.ToString() : string.Empty);
            }
            else
            {
                throw new Exception("Expected ObjectId value.");
            }
        }
    }

}
