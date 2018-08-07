using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Core.Extensions
{
    public static class StringAcento
    {
        public static string RemoverAcento(this string texto)
        {
            char[] replacement = { 'a', 'a', 'a', 'a', 'a', 'a', 'c', 'e', 'e', 'e', 'e', 'i', 'i', 'i', 'i', 'n', 'o', 'o', 'o', 'o', 'o', 'u', 'u', 'u', 'u', 'y', 'y' };
            char[] accents = { 'à', 'á', 'â', 'ã', 'ä', 'å', 'ç', 'é', 'è', 'ê', 'ë', 'ì', 'í', 'î', 'ï', 'ñ', 'ò', 'ó', 'ô', 'ö', 'õ', 'ù', 'ú', 'û', 'ü', 'ý', 'ÿ' };


            for (int i = 0; i < accents.Length; i++)
            {
                texto = texto.Replace(accents[i], replacement[i]);
            }

            return texto;
        }
    }
}
