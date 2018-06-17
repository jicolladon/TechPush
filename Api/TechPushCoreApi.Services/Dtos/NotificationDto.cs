using System;
using System.Collections.Generic;
using System.Text;

namespace TechPushCoreApi.Services.Dtos
{
    public class NotificationDto
    {
        public Guid Guid { get; set; }

        public byte[] Image
        {
            get; set;

        }

        public string Tag { get; set; }

        public string Message
        {
            get; set;
        }


        public DateTime Date
        {
            get; set;

        }
    }
}
