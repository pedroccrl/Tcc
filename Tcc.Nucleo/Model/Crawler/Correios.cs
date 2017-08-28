using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;
namespace Tcc.Nucleo.Model.Crawler
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
                foreach (HtmlNode table in doc.DocumentNode.Descendants("//table"))
                {
                    foreach (HtmlNode row in table.Descendants("tr"))
                    {
                        if (r++ < 2) continue;
                        var logra = new Logradouro();
                        logra.Nome = WebUtility.HtmlDecode(row.Descendants("th|td").ToList()[0].InnerText);
                        //logra.bairro = WebUtility.HtmlDecode(row.SelectNodes("th|td")[1].InnerText);
                        //logra.cidade = WebUtility.HtmlDecode(row.SelectNodes("th|td")[2].InnerText);
                        //logra.cep = WebUtility.HtmlDecode(row.SelectNodes("th|td")[3].InnerText);
                        ls.Add(logra);
                    }
                }
            }

            
            
        }

        public static async Task<Bairro> GetLogradouroPorBairroAsync(Cidade cidade, string bairro, IStatus status = null)
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

                http.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36 Edge/15.15063");
                http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                http.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                http.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, image/jxr, */*");
                http.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "pt-BR,pt;q=0.5");
                var bairroEncoded = StringAcento.RemoverAcento(bairro.ToLower().Trim());
                //var bairroEncoded = Uri.EscapeUriString(bairro.Trim());
                for (int i = 1; i < 1000; i += 50)
                {
                    var forms = new List<KeyValuePair<string, string>>();
                    forms.Add(new KeyValuePair<string, string>("Bairro", bairroEncoded));
                    forms.Add(new KeyValuePair<string, string>("Localidade", cidade.Nome));
                    forms.Add(new KeyValuePair<string, string>("UF", cidade.UF.ToUpper()));
                    forms.Add(new KeyValuePair<string, string>("qtdrow", "50"));
                    forms.Add(new KeyValuePair<string, string>("pagini", $"{i}"));
                    forms.Add(new KeyValuePair<string, string>("pagfim", $"{i + 50}"));

                    status?.Escrever($"Requisitando {bairro.Trim()}, Pagina: {i}");
                    var resposta = await http.PostAsync("http://www.buscacep.correios.com.br/sistemas/buscacep/resultadoBuscaLogBairro.cfm", new FormUrlEncodedContent(forms));


                    var resultado = await resposta.Content.ReadAsStringAsync();
                    status?.Escrever($"Resposta tamanho {resultado.Length}");
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(resultado);


                    int r = 0;
                    var nodes = doc.DocumentNode.Descendants("table").ToList();
                    var c = nodes.Count();
                    if (nodes == null) break;

                    try
                    {
                        var div = doc.DocumentNode.Descendants("div")?.Where(e => e.Attributes["class"].Value == "ctrlcontent").First()?.Descendants("p")?.First()?.InnerText;
                        if (div == "BAIRRO/LOGRADOURO NAO ENCONTRADO.") break;
                        status?.Escrever(div);
                    }
                    catch (Exception)
                    {
                        break;
                    }

                    

                    foreach (HtmlNode table in nodes)
                    {
                        Debug.WriteLine("Found: " + table.Id);
                        status?.Escrever($"Tabela encontrada");
                        foreach (HtmlNode row in table.Descendants("tr").ToList())
                        {
                            if (r++ == 0) continue;
                            var logra = new Logradouro();
                            var linha = row.Descendants("td").ToList();
                            //if (linha.Count != 4)
                            //{
                            //    i = 2000;
                            //    break;
                            //}
                            //status?.Escrever($"{linha.Count} colunas encontradas");
                            try
                            {
                                logra.Nome = WebUtility.HtmlDecode(linha[0].InnerText);
                                logra.Cep = WebUtility.HtmlDecode(linha[3].InnerText);
                                b.Logradouros.Add(logra);
                                status?.Escrever($"Logradouro: {logra.Nome} encontrado");
                            }
                            catch (Exception e)
                            {
                                
                            }
                            
                        }
                    }
                    status?.Escrever($"Aguardando 10 segundos");
                    await Task.Delay(1000 * 10);
                }
            });
            return b;
        }
    }
}
