using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model;
using static System.Console;
namespace Tcc.Comando.Model
{
    public class Status : IStatus
    {
        public void Escrever(string texto)
        {
            SetCursorPosition(0, CursorTop - 1);
            WriteLine(texto);
        }
    }
}
