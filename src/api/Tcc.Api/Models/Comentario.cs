using Newtonsoft.Json;

namespace Tcc.Api.Models
{
    namespace Tcc.Api.Models
    {
        public partial class Comentario
        {
            [JsonProperty("_id")]
            public string Id { get; set; }

            [JsonProperty("created_time")]
            public string CreatedTime { get; set; }

            [JsonProperty("from")]
            public From From { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("like_count")]
            public long LikeCount { get; set; }

            [JsonProperty("comments")]
            public object Comments { get; set; }

            [JsonProperty("IdComentario")]
            public long IdComentario { get; set; }

            [JsonProperty("IdRespondido")]
            public object IdRespondido { get; set; }

            [JsonProperty("IdCidade")]
            public long IdCidade { get; set; }

            [JsonProperty("IdPagina")]
            public long IdPagina { get; set; }

            [JsonProperty("IdPost")]
            public long IdPost { get; set; }

            [JsonProperty("palavras_unk")]
            public object[] PalavrasUnk { get; set; }

            [JsonProperty("corrigido")]
            public string Corrigido { get; set; }

            [JsonProperty("palavras_unk_c")]
            public object[] PalavrasUnkC { get; set; }

            [JsonProperty("tema")]
            public Tema Tema { get; set; }

            [JsonProperty("TemLogradouro")]
            public bool TemLogradouro { get; set; }

            [JsonProperty("Logradouros")]
            public Logradouro[] Logradouros { get; set; }
        }

        public partial class From
        {
            [JsonProperty("_id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public partial class Logradouro
        {
            [JsonProperty("IdBairro")]
            public long IdBairro { get; set; }

            [JsonProperty("Nome")]
            public string Nome { get; set; }

            [JsonProperty("Cep")]
            public string Cep { get; set; }

            [JsonProperty("Longitude")]
            public double Longitude { get; set; }

            [JsonProperty("_id")]
            public long Id { get; set; }

            [JsonProperty("Latitude")]
            public double Latitude { get; set; }

            [JsonProperty("Tipo")]
            public string Tipo { get; set; }

            [JsonProperty("Bairro")]
            public Bairro Bairro { get; set; }

            [JsonProperty("IdCidade")]
            public long IdCidade { get; set; }
        }

        public partial class Bairro
        {
            [JsonProperty("NomeAlternativo")]
            public string NomeAlternativo { get; set; }

            [JsonProperty("Nome")]
            public string Nome { get; set; }

            [JsonProperty("Cidade")]
            public string Cidade { get; set; }
        }

        public partial class Tema
        {
            [JsonProperty("nomes")]
            public string[] Nomes { get; set; }

            [JsonProperty("qualidades")]
            public string[] Qualidades { get; set; }

            [JsonProperty("pos_tags")]
            public string[][][] PosTags { get; set; }
        }
    }

}
