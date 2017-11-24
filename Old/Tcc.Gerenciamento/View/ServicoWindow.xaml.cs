using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tcc.Gerenciamento.Model;

namespace Tcc.Gerenciamento.View
{
    /// <summary>
    /// Lógica interna para ServicoWindow.xaml
    /// </summary>
    public partial class ServicoWindow : Window
    {
        public ServicoWindow()
        {
            InitializeComponent();
            var serv = new Servico();
            DataContext = serv;
            serv.InitAsync();
        }
    }
}
