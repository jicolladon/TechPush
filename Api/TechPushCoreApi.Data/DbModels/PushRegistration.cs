using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechPushCoreApi.Data.DbModels
{
    [Table("PushRegistrations")]

    public class PushRegistration
    {
        public PushRegistration()
        {
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string DeviceId { get; set; }

        [Required]
        public string Token { get; set; }

        public DateTime UpdateDate { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public string PlatformType { get; set; }

        public string Tags { get; set; }

    }
}
