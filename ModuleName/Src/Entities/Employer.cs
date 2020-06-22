using GOI.Services.Common.Entities;
using System;

namespace GOI.Seeker.Master.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Employer : EntityBase
    {
        public string Name { get; set; }
        public Guid? AddressId { get; set; }

        //Properties to be added
    }
}