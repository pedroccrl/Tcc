using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model;
namespace Tcc.Gerenciamento.ViewModel
{
    public class NovaCidadeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Cidade _cidade;

        public Cidade Cidade
        {
            get { return _cidade; }
            set { _cidade = value; Notify("Cidade"); }
        }

        private string _filename;

        public string FileName
        {
            get { return _filename; }
            set { _filename = value; Notify("FileName"); }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; Notify("Status"); }
        }

        public NovaCidadeViewModel()
        {
            Cidade = new Cidade("", "RJ");
        }

        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


    }
}
