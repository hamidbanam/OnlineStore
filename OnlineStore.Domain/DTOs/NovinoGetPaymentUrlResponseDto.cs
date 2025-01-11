﻿using Newtonsoft.Json;

namespace OnlineStore.Domain.DTOs
{
    public class NovinoGetPaymentUrlResponseDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public NovinoGetPaymentUrlRequestData Data { get; set; }

        [JsonProperty("errors")]
        public object Errors { get; set; }
    }

    public class NovinoGetPaymentUrlRequestData
    {
        [JsonProperty("wage")]
        public int Wage { get; set; }

        [JsonProperty("wage_payer")]
        public string WagePayer { get; set; }

        [JsonProperty("authority")]
        public string Authority { get; set; }

        [JsonProperty("payment_url")]
        public string PaymentUrl { get; set; }
    }
}
