using System;
using System.Collections.Generic;
using System.Text;
using TechPushCoreApi.Data.DbModels;

namespace TechPushCoreApi.Data.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        IEnumerable<PushMessage> GetMessagesOfUser(string username);
        void InsertMessage(PushMessage message);
        bool SaveChanges();
    }
}
