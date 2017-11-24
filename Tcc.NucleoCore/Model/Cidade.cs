using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace Tcc.Nucleo.Model
{
    public class Cidade : INotifyPropertyChanged
    {
        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; Notify("Nome"); }
        }

        private string _uf;

        public string UF
        {
            get { return _uf; }
            set { _uf = value; Notify("UF"); }
        }

        public List<Bairro> Bairros { get; set; }
        public IFile File { get; set; }

        public Cidade(string nome, string uf)
        {
            Nome = nome;
            UF = uf;
            Bairros = new List<Bairro>();
        }

        public Cidade()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void BuscaLogradourosDoCorreios()
        {
            if (Bairros == null || Bairros.Count == 0) throw new Exception("Sem bairros");
        }

        public async Task LoadBairrosFromCsv(string fileName, int indiceAttributo = 1)
        {

            var linhas = (await File.LoadFileAsync(fileName)).Split('\n'); ;
            for (int i = 1; i < linhas.Length-1; i++)
            {
                try
                {
                    var linha = linhas[i];
                    var nomeBairro = linha.Split(',')[indiceAttributo].Replace("\"", "").Replace("\r", "");
                    var bairro = new Bairro { Nome = nomeBairro };
                    Bairros.Add(bairro);
                }
                catch (Exception e)
                {

                    throw;
                }
                
            }
        }

        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
