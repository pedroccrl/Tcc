using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Tcc.Api.Converters;

namespace Tcc.Api.Models
{
    public class Comentario
    {
        public string _id { get; set; }
        public string created_time { get; set; }
        public From from { get; set; }
        public string message { get; set; }
        public int like_count { get; set; }
        public object comments { get; set; }
        public int IdComentario { get; set; }
        public object IdRespondido { get; set; }
        public int IdCidade { get; set; }
        public int IdPagina { get; set; }
        public int IdPost { get; set; }
        public object[] palavras_unk { get; set; }
        public string corrigido { get; set; }
        public object[] palavras_unk_c { get; set; }
        public Tema tema { get; set; }
        public bool TemLogradouro { get; set; }
        public Logradouro[] Logradouros { get; set; }
        public object nome { get; set; }
    }

    public class From
    {
        public string _id { get; set; }
        public string name { get; set; }
    }

    public class Tema
    {
        public string[] nomes { get; set; }
        public string[] qualidades { get; set; }
        public string[][][] pos_tags { get; set; }
    }

    public class Logradouro
    {
        public int IdBairro { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public object Longitude { get; set; }
        public int _id { get; set; }
        public object Latitude { get; set; }
        public string Tipo { get; set; }
        public Bairro Bairro { get; set; }
        public int IdCidade { get; set; }
    }

    public class Bairro
    {
        public string NomeAlternativo { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
    }

}
