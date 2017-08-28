using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Shared.Model;

namespace Tcc.Shared.ViewModel
{
    public class CidadesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Cidade> _cidades;

        public List<Cidade> Cidade
        {
            get { return _cidades; }
            set { _cidades = value; Notify("Cidades"); }
        }

        void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
