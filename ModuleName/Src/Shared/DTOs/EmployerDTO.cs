using System;
using GOI.Services.Common.DTOs;
using Newtonsoft.Json;

namespace GOI.Seeker.Master.Shared.DTOs
{
    public class EmployerDTO : BaseDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("addressId")]
        public Guid? AddressId { get; set; }

    }
}