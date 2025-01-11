using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Security
{
    public static class SecretCode
    {
        public static int RandomString()
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            return randomNumber;

        }
    }
}
