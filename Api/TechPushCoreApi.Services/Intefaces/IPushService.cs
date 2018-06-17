using System;
using System.Collections.Generic;
using System.Text;
using TechPushCoreApi.Data.DbModels;

namespace TechPushCoreApi.Services.Intefaces
{
    public interface IPushService
    {
        void SendNotification(PushSendEntity entity, string rootPath);
    }
}
