﻿using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace OnlineStore.Domain.DTOs
{
    public class NovinoGetPaymentUrlRequestDto
    {
        [JsonProperty("merchant_id")]
        public string MerchantId { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("callback_url")]
        public string CallBackUrl { get; set; }

        [JsonProperty("callback_method")]
        public string CallBackMethod { get; set; }

        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("card_pan")]
        public string CardPan { get; set; }
    }
}
