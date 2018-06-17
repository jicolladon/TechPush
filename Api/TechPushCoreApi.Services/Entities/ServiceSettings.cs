using System;
using System.Collections.Generic;
using System.Text;

namespace TechPushCoreApi.Services.Entities
{
    public class ServiceSettings
    {
        public string IOSCertificatePath { get; set; }
        public string AndroidToken { get; set; }
        public string IOSPassCertificate { get; set; }
        public string AndroidSender { get; set; }
        public string IOSTypeCertificate { get; set; }
        public int RandomQt { get; set; }
        public string AppKey { get; set; }

    }
}
