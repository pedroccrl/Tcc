using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Shared.Model.Crawler
{
    public static class Correios
    {
        public static async void BuscaCepEndereço(string query)
        {
            var http = new HttpClient();

            http = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                AllowAutoRedirect = true,
                
            });

            http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
            http.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            http.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, image/jxr, */*");

            var mongo = new MongoClient("mongodb://localhost:27017");
            var db = mongo.GetDatabase("logradouros");
            var coll = db.GetCollection<Logradouro>("rio das ostras");
            var ls = new List<Logradouro>();
            for (int i = 1; i < 1000; i+=50)
            {
                var forms = new List<KeyValuePair<string, string>>();
                forms.Add(new KeyValuePair<string, string>("relaxation", "rio das ostras, rj"));
                forms.Add(new KeyValuePair<string, string>("tipoCEL", "ALL"));
                forms.Add(new KeyValuePair<string, string>("semelhante", "N"));
                forms.Add(new KeyValuePair<string, string>("qtdrow", "50"));
                forms.Add(new KeyValuePair<string, string>("pagini", $"{i}"));
                forms.Add(new KeyValuePair<string, string>("pagfim", $"{i+50}"));

                var resposta = await http.PostAsync("http://www.buscacep.correios.com.br/sistemas/buscacep/resultadoBuscaCepEndereco.cfm", new FormUrlEncodedContent(forms));


                var resultado = await resposta.Content.ReadAsStringAsync();

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(resultado);


                int r = 0;
                foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
                {
                    Console.WriteLine("Found: " + table.Id);
                    foreach (HtmlNode row in table.SelectNodes("tr"))
                    {
                        Console.WriteLine("row");
                        if (r++ < 2) continue;
                        var logra = new Logradouro();
                        logra.Nome = WebUtility.HtmlDecode(row.SelectNodes("th|td")[0].InnerText);
                        //logra.bairro = WebUtility.HtmlDecode(row.SelectNodes("th|td")[1].InnerText);
                        //logra.cidade = WebUtility.HtmlDecode(row.SelectNodes("th|td")[2].InnerText);
                        //logra.cep = WebUtility.HtmlDecode(row.SelectNodes("th|td")[3].InnerText);
                        ls.Add(logra);
                        coll.InsertOne(logra);
                    }
                }
            }

            
            
        }

        public static async void BuscaLogradouroPorBairro(string bairro, string cidade, string uf)
        {
            var http = new HttpClient();

            http = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                AllowAutoRedirect = true,
            });

            http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
            http.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            http.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, image/jxr, */*");

            var mongo = new MongoClient("mongodb://localhost:27017");
            var db = mongo.GetDatabase("logradouros");
            var coll = db.GetCollection<Logradouro>("rio das ostras");
            var ls = new List<Logradouro>();
            for (int i = 1; i < 1000; i += 50)
            {
                var forms = new List<KeyValuePair<string, string>>();
                forms.Add(new KeyValuePair<string, string>("Bairro", bairro.Trim()));
                forms.Add(new KeyValuePair<string, string>("Localidade", cidade));
                forms.Add(new KeyValuePair<string, string>("UF", uf.ToUpper()));
                forms.Add(new KeyValuePair<string, string>("qtdrow", "50"));
                forms.Add(new KeyValuePair<string, string>("pagini", $"{i}"));
                forms.Add(new KeyValuePair<string, string>("pagfim", $"{i + 50}"));

                var resposta = await http.PostAsync("http://www.buscacep.correios.com.br/sistemas/buscacep/resultadoBuscaLogBairro.cfm", new FormUrlEncodedContent(forms));


                var resultado = await resposta.Content.ReadAsStringAsync();

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(resultado);


                int r = 0;
                var nodes = doc.DocumentNode.SelectNodes("//table");
                if (nodes == null) break;
                foreach (HtmlNode table in nodes)
                {
                    //Console.WriteLine("Found: " + table.Id);
                    foreach (HtmlNode row in table.SelectNodes("tr"))
                    {
                        Console.WriteLine("row");
                        if (r++ < 2) continue;
                        var logra = new Logradouro();
                        //logra.nome = WebUtility.HtmlDecode(row.SelectNodes("th|td")[0].InnerText);
                        //logra.bairro = WebUtility.HtmlDecode(row.SelectNodes("th|td")[1].InnerText);
                        //logra.cidade = WebUtility.HtmlDecode(row.SelectNodes("th|td")[2].InnerText);
                        //logra.cep = WebUtility.HtmlDecode(row.SelectNodes("th|td")[3].InnerText);
                        ls.Add(logra);
                        coll.InsertOne(logra);
                    }
                }
            }
        }

        public static async Task<Bairro> GetLogradouroPorBairroAsync(Cidade cidade, string bairro)
        {
            var b = new Bairro { Nome = bairro, Logradouros = new List<Logradouro>() };
            await Task.Run(async() =>
            {
                var http = new HttpClient();

                http = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    AllowAutoRedirect = true,
                });

                http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                http.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                http.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, image/jxr, */*");

                for (int i = 1; i < 1000; i += 50)
                {
                    var forms = new List<KeyValuePair<string, string>>();
                    forms.Add(new KeyValuePair<string, string>("Bairro", bairro.Trim()));
                    forms.Add(new KeyValuePair<string, string>("Localidade", cidade.Nome));
                    forms.Add(new KeyValuePair<string, string>("UF", cidade.UF));
                    forms.Add(new KeyValuePair<string, string>("qtdrow", "50"));
                    forms.Add(new KeyValuePair<string, string>("pagini", $"{i}"));
                    forms.Add(new KeyValuePair<string, string>("pagfim", $"{i + 50}"));

                    var resposta = await http.PostAsync("http://www.buscacep.correios.com.br/sistemas/buscacep/resultadoBuscaLogBairro.cfm", new FormUrlEncodedContent(forms));


                    var resultado = await resposta.Content.ReadAsStringAsync();

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(resultado);


                    int r = 0;
                    var nodes = doc.DocumentNode.SelectNodes("//table");
                    if (nodes == null) break;
                    foreach (HtmlNode table in nodes)
                    {
                        //Console.WriteLine("Found: " + table.Id);
                        foreach (HtmlNode row in table.SelectNodes("tr"))
                        {
                            Console.WriteLine("row");
                            if (r++ < 2) continue;
                            var logra = new Logradouro();
                            logra.Nome = WebUtility.HtmlDecode(row.SelectNodes("th|td")[0].InnerText);
                            //logra.bairro = WebUtility.HtmlDecode(row.SelectNodes("th|td")[1].InnerText);
                            //logra.cidade = WebUtility.HtmlDecode(row.SelectNodes("th|td")[2].InnerText);
                            logra.Cep = WebUtility.HtmlDecode(row.SelectNodes("th|td")[3].InnerText);

                        }
                    }
                }
            });
            return b;
        }
    }
}
