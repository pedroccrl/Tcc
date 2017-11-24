using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Tcc.Shared.Model
{
    public class Cidade
    {
        public string Nome { get; set; }
        public string UF { get; set; }
        public List<Bairro> Bairros { get; set; }

        public Cidade(string nome, string uf)
        {
            Nome = nome;
            UF = uf;
            Bairros = new List<Bairro>();
        }

        public void BuscaLogradourosDoCorreios()
        {
            if (Bairros == null || Bairros.Count == 0) throw new Exception("Sem bairros");
        }

        public void LoadBairrosFromCsv(string fileName)
        {
            var linhas = File.ReadAllLines(fileName);
            for (int i = 1; i < linhas.Length; i++)
            {
                var linha = linhas[i];
                var nomeBairro = linha.Split(',')[1].Replace("\"", "");
                var bairro = new Bairro { Nome = nomeBairro };
                Bairros.Add(bairro);
            }
        }
    }
}
