using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Tcc.Api.Converters;

namespace Tcc.Api.Models
{
    public class Cidade
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        [BsonElement("_id")]
        [JsonProperty("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("nome")]
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [BsonElement("idCidade")]
        [JsonProperty("idCidade")]
        public long IdCidade { get; set; }

        [BsonElement("temas")]
        [JsonProperty("temas")]
        public object[][] Temas { get; set; }

        [BsonElement("qualidades")]
        [JsonProperty("qualidades")]
        public object[][] Qualidades { get; set; }
    }
}
