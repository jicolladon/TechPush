using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechPushCoreApi.Data.DbModels
{
    [Table("UserData")]
    public class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            PushRegistration = new HashSet<PushRegistration>();
        }

        public Guid Id { get; set; }

        [StringLength(100)]
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassword { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<PushRegistration> PushRegistration { get; set; }

        public virtual ICollection<PushMessage> Messages { get; set; }
    }
}