using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model;
using System.IO;
namespace Tcc.Gerenciamento.Model
{
    public class Arquivo : IFile
    {
        public async Task<string> LoadFileAsync(string filename)
        {
            var arq = string.Empty;
            await Task.Run(() =>
            {
                arq = File.ReadAllText(filename);
            });
            return arq;
        }
    }
}
