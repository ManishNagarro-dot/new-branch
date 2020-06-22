using System;
using Newtonsoft.Json;

namespace gov.seeker.moduleName.Shared.DTOs
{
    public class BaseDTO : IBaseDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("correlationId")]
        public Guid CorrelationId { get; set; }

        [JsonProperty("lastModifiedAt")]
        public DateTime LastModifiedAt { get; set; }
    }
}