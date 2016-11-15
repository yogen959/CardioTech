using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardioTech.Models
{
    class Products
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "indications")]
        public string indications { get; set; }

        [JsonProperty(PropertyName = "ingredient")]
        public string ingredient { get; set; }

        [JsonProperty(PropertyName = "thumb")]
        public string thumb { get; set; }

        [JsonProperty(PropertyName = "medium")]
        public string medium { get; set; }

    }
}
