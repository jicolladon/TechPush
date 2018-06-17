using System;
using System.Collections.Generic;
using System.Text;

namespace TechPushCoreApi.Services.Dtos
{
    public class PushMessageDto : BaseDto
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<string> Tag { get; set; }
        public byte[] Image { get; set; }
        public List<string> Genders { get; set; }
        public List<int> Ages { get; set; }
        public bool SelectRandom { get; set; }
        public string Title { get; set; }
    }
}
