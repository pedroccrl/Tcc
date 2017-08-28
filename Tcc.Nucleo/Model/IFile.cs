using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model
{
    public interface IFile
    {
        Task<string> LoadFileAsync(string filename);
    }
}
