using System;
using System.Collections.Generic;
using System.Text;

namespace TechPush.Core.Dto
{
    public class MessageDto
    {

        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Tag { get; set; }
        public byte[] Image { get; set; }
    }
}
