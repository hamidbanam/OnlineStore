using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Static
{
    public class SocialMedia
    {
        [JsonProperty("Telegram")]
        public static string Telegram { get; set; }

        [JsonProperty("Instagram")]
        public static string Instagram { get; set; }

        [JsonProperty("Whatsapp")]
        public static string Whatsapp { get; set; }

    }
}
