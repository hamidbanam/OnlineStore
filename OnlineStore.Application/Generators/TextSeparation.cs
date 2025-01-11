using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Generators
{
    public static class TextSeparation
    {
        public static List<string> SplitWords(this string text)
        {
            return text.Split(new char[] { ',', '-', '_', '،' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
        }
    }
}
