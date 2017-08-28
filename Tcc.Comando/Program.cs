using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model;
using Tcc.MySQL.Model;
using Tcc.Nucleo.Model.Crawler;
using System.Diagnostics;
using Tcc.Comando.Model;
using Tcc.Nucleo.Model.Analise;

namespace Tcc.Comando
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("F1 - Inserir Cidade");
                Console.WriteLine("F2 - Buscar Logradouros do Bairro/Cidade pelo Crawler dos Correios");
                Console.WriteLine("F3 - Obter Coordenadas do logradouro");
                Console.WriteLine("====================================");
                Console.WriteLine("F4 - Adicionar página de facebook");
                Console.WriteLine("F6 - Adicionar posts página de facebook");
                Console.WriteLine("F7 - Adicionar comentarios de posts da página de facebook");
                Console.WriteLine("====================================");
                Console.WriteLine("F8 - Procurar locais em posts");
                Console.WriteLine("F9 - Procurar locais em comentarios");
                Console.WriteLine("F10 - Tokenize bairro x post");
                Console.WriteLine("====================================");
                Console.WriteLine("\n\nESC - SAIR");
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.F1:
                        InserirCidade();
                        break;
                    case ConsoleKey.F2:
                        BuscarLogradouros();
                        break;
                    case ConsoleKey.F3:
                        break;
                    case ConsoleKey.F4:
                        Facebook.AdicionarPagina().Wait();
                        break;
                    case ConsoleKey.F6:
                        Facebook.AdicionarPosts().Wait();
                        break;
                    case ConsoleKey.F7:
                        Facebook.AdicionarComentarios().Wait();
                        break;
                    case ConsoleKey.F8:
                        var postsDao = PostFacebookDAO.BuscarTodosPosts();
                        var posts = new List<Nucleo.Model.Facebook.Post>();
                        foreach (var post in postsDao)
                        {
                            var p = new Nucleo.Model.Facebook.Post();
                            p = post;
                            posts.Add(p);
                        }
                        Analisador.ContaLocalNoPost(posts, new Status());
                        break;
                    case ConsoleKey.F9:
                        var comentarios = ComentarioFacebookDAO.BuscarTodosComentarios();
                        Analisador.ContaLocalNosComentarios(comentarios, new Status());
                        break;
                    case ConsoleKey.F12:
                        Analise.AnalisarLogradourosEmPosts();
                        break;
                    case ConsoleKey.Escape:
                        loop = false;
                        break;
                    default:
                        break;
                }

            }
        }

        static void BuscarLogradouros()
        {
            Console.Write("Cidade: ");
            var nome = Console.ReadLine();
            var cidade = CidadeDAO.BuscarCidade(nome);
            Console.WriteLine($"{cidade.Nome} tem {cidade.BairrosDAO.Count} bairros.");
            Console.WriteLine("F1 - Mostrar bairros e/ou buscar logradouros individualmente");
            Console.WriteLine("F2 - Buscar logradouros de todos os bairros");
            Console.WriteLine("F3 - Mostrar bairros sem logradouros e/ou buscar logradouros individualmente");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.F1:
                    MostrarBairros(cidade);
                    break;
                case ConsoleKey.F2:
                    BuscarLogradourosBairros(cidade);
                    break;
                case ConsoleKey.F3:
                    cidade.BairrosDAO = BairroDAO.BuscarBairrosSemLogradouros(cidade);
                    MostrarBairros(cidade);
                    break;
                default: break;
            }
        }

        static void MostrarBairros(CidadeDAO cidade, bool semLogradouro = false)
        {
            int ini = 0;
            int fim = 10;
            int q = 10;
            if (fim > cidade.BairrosDAO.Count)
                fim = cidade.BairrosDAO.Count;
            Console.WriteLine("Use as setas <- e -> para exibir ou ESC para sair.");
            Console.WriteLine("Para buscar individualmente aperte TAB e o id do bairro.");
            ConsoleKey key = ConsoleKey.A;
            while (key != ConsoleKey.Escape)
            {
                Console.WriteLine("ID\tID Banco\tBairro");
                for (int i = ini; i < fim; i++)
                {
                    Console.WriteLine($"{i}\t{cidade.BairrosDAO[i].Id}\t{cidade.BairrosDAO[i].Nome}");
                }
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.RightArrow)
                {
                    if (fim + q > cidade.BairrosDAO.Count)
                    {
                        fim = cidade.BairrosDAO.Count;
                        ini = fim - (fim-ini);
                    }
                    else
                    {
                        ini += q;
                        fim += q;
                    }
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    if (ini - q < 0)
                    {
                        ini = 0;
                        fim = q;
                        if (fim > cidade.BairrosDAO.Count)
                            fim = cidade.BairrosDAO.Count;
                    }
                    else
                    {
                        ini -= q;
                        fim -= q;
                    }
                }
                else if (key == ConsoleKey.Tab)
                {
                    Console.Write("ID: ");
                    var id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("A busca pode demorar alguns minutos. Aguarde...");
                    Bairro b = null;
                    var sw = new Stopwatch();
                    sw.Start();
                    var status = new Status();
                    Task.Run(async () =>
                    {
                        var city = new Cidade(cidade.Nome, cidade.UF);
                        b = await Correios.GetLogradouroPorBairroAsync(city, cidade.BairrosDAO[id].Nome, status);
                    }).Wait();
                    sw.Stop();
                    
                    foreach (var log in b.Logradouros)
                    {
                        LogradouroDAO.AdicionarLogradouro(log, cidade.Id, cidade.BairrosDAO[id].Id);
                    }
                    Console.WriteLine($"A busca retornou {b.Logradouros.Count} logradouros e demorou {sw.Elapsed.TotalMinutes:0.0} min");
                }
            }
        }

        static void BuscarLogradourosBairros(CidadeDAO cidade)
        {
            Console.WriteLine("A busca pode demorar alguns minutos. Aguarde...");
            Bairro b = null;
            var sw = new Stopwatch();
            
            for (int id = 39; id < cidade.BairrosDAO.Count; id++)
            {
                sw.Start();
                Task.Run(async () =>
                {
                    var city = new Cidade(cidade.Nome, cidade.UF);
                    b = await Correios.GetLogradouroPorBairroAsync(city, cidade.BairrosDAO[id].Nome, new Status());
                }).Wait();
                foreach (var log in b.Logradouros)
                {
                    LogradouroDAO.AdicionarLogradouro(log, cidade.Id, cidade.BairrosDAO[id].Id);
                    Console.WriteLine($"{log.Nome} - {log.Cep} adicionado ao bd");
                }
                sw.Stop();
                Console.WriteLine($"A busca do {b.Nome} retornou {b.Logradouros.Count} logradouros e demorou {sw.Elapsed.TotalMinutes:0.0} min");
                Console.WriteLine("Aguardando 5 seg para buscar novamente");
                Task.Delay(1000 * 5 * 1).Wait();
            }
            
        }


        static void InserirCidade()
        {
            Console.Write("Digite nome e UFF: ");
            var nome = Console.ReadLine();
            var uf = Console.ReadLine();
            Console.Write("Arquivo .CSV dos bairros: ");
            var arquivo = Console.ReadLine();
            var cidade = new Cidade(nome, uf) { File = new Model.Arquivo() };
            cidade.LoadBairrosFromCsv(arquivo).Wait();
            Console.WriteLine($"\n{cidade.Bairros.Count} bairros encontrados\n");

            Console.WriteLine("1 - Adicionar Cidade\n2 - Sair");
            if (int.Parse(Console.ReadLine())==2)
                return;

            CidadeDAO.AdicionarCidade(cidade);
        }
    }
}
