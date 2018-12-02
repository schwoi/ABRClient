using System.Collections.Generic;
using Newtonsoft.Json;

namespace AbrApi.Models
{
    public class SearchResult
    {

        [JsonProperty("Message")] public string Message { get; set; }

        [JsonProperty("Names")] public List<Name> Names { get; set; }
    }

    public class Name
    {
        [JsonProperty("Abn")] public string Abn { get; set; }

        [JsonProperty("AbnStatus")] public string AbnStatus { get; set; }

        [JsonProperty("IsCurrent")] public bool IsCurrent { get; set; }

        [JsonProperty("Name")] public string NameName { get; set; }

        [JsonProperty("NameType")] public string NameType { get; set; }

        [JsonProperty("Postcode")] public string Postcode { get; set; }

        [JsonProperty("Score")] public long Score { get; set; }

        [JsonProperty("State")] public string State { get; set; }
    }

}