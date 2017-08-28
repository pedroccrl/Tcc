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
using Tcc.Gerenciamento.ViewModel;
namespace Tcc.Gerenciamento.View
{
    /// <summary>
    /// Interaction logic for NovaCidadeWindow.xaml
    /// </summary>
    public partial class NovaCidadeWindow : Window
    {
        NovaCidadeViewModel viewModel;
        public NovaCidadeWindow()
        {
            InitializeComponent();
            viewModel = new NovaCidadeViewModel();
            DataContext = viewModel;
        }
    }
}
