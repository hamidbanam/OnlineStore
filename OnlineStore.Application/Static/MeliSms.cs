using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Static
{
    public class MeliSms
    {
        [JsonProperty("UserName")]
        public static string UserName { get; set; }

        [JsonProperty("Password")]
        public static string Password { get; set; }

        [JsonProperty("Sender")]
        public static string From { get; set; }
    }
}
