using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Extensions
{
    public class NameExtentions
    {
        public static string GenerateUnicCod()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
