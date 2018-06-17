using System;
using System.Collections.Generic;
using System.Text;
using TechPushCoreApi.Services.Enum;

namespace TechPushCoreApi.Services.Dtos
{
    public class PushRegistrationDto : BaseDto
    {
        public string DeviceToken { get; set; }
        public string DeviceId { get; set; }
        public DeviceTypeEnum DeviceType { get; set; }
        public List<string> Tags { get; set; }
    }
}
