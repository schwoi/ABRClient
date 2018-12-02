using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AbrApi.Models
{
    public class EntityDetail
    {
        [JsonProperty("Abn")]
        public string Abn { get; set; }

        [JsonProperty("AbnStatus")]
        public string AbnStatus { get; set; }

        [JsonProperty("AddressDate")]
        public DateTimeOffset AddressDate { get; set; }

        [JsonProperty("AddressPostcode")]
        public string AddressPostcode { get; set; }

        [JsonProperty("AddressState")]
        public string AddressState { get; set; }

        [JsonProperty("BusinessName")]
        public List<string> BusinessName { get; set; }

        [JsonProperty("EntityName")]
        public string EntityName { get; set; }

        [JsonProperty("EntityTypeCode")]
        public string EntityTypeCode { get; set; }

        [JsonProperty("EntityTypeName")]
        public string EntityTypeName { get; set; }

        [JsonProperty("Gst")]
        public DateTimeOffset Gst { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}