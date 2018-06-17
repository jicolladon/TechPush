using System;
using System.Collections.Generic;
using System.Text;
using TechPush.Core.Enum;

namespace TechPush.Core.Dto
{
    public class PushRegistrationDto : BaseDto
    {
        public string DeviceToken { get; set; }
        public string DeviceId { get; set; }
        public DeviceTypeEnum DeviceType { get; set; }
        public List<string> Tags { get; set; }
    }
}
