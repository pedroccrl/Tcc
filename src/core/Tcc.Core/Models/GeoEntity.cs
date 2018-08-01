using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Core.Models
{
    public class GeoEntity : Entity
    {
        [JsonIgnore]
        public GeoJsonPoint<GeoJson2DCoordinates> Localization { get; set; }

        [BsonIgnore]
        public double Lng { get => Localization == null ? 0 : Localization.Coordinates.X; }

        [BsonIgnore]
        public double Lat { get => Localization == null ? 0 : Localization.Coordinates.Y; }
    }
}
