using System;
using Newtonsoft.Json;

namespace GOI.seeker.moduleName.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityBase : IEntityBase
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "lastModifiedAt")]
        public DateTime? LastModifiedAt { get; set; } = DateTime.Now;

    }
}