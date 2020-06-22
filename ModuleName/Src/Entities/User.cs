using GOI.Services.Common.Entities;
using System;

namespace GOI.Seeker.Master.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class User : EntityBase
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Guid? SkillId { get; set; }
        public Guid? AddressId { get; set; }

        //Properties to be added
    }
}