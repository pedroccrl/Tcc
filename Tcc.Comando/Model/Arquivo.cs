using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model;

namespace Tcc.Comando.Model
{
    public class Arquivo : IFile
    {
        public async Task<string> LoadFileAsync(string filename)
        {
            var arq = string.Empty;
            await Task.Run(() =>
            {
                arq = File.ReadAllText(filename, Encoding.Default);
            });
            return arq;
        }
    }
}
