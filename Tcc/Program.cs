using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Shared.Model;
using Tcc.Shared.Model.Crawler;

namespace Tcc
{
    class Program
    {
        static void Main(string[] args)
        {
            //Correios.BuscaCepEndereço("rio das ostras, rj");
            var ro = new Cidade("Rio das Ostras", "RJ");
            ro.LoadBairrosFromCsv("bairros_riodasostras.csv");
            Console.Read();
        }
    }
}
