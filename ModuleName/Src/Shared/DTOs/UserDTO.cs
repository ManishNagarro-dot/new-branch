using System;
using GOI.Services.Common.DTOs;
using Newtonsoft.Json;

namespace GOI.Seeker.Master.Shared.DTOs
{
    public class UserDTO : BaseDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }


        [JsonProperty("skillId")]
        public Guid? SkillId { get; set; }

        [JsonProperty("addressId")]
        public Guid? AddressId { get; set; }
    }
}