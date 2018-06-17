using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechPushCoreApi.Data.DbModels
{
    [Table("Messages")]
    public class PushMessage
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Tag { get; set; }
        [Required]
        public virtual User User { get; set; }
        public byte[] Image { get; set; }
    }
}
