using System;
using System.Collections.Generic;
using System.Text;
using TechPushCoreApi.Data.DbModels;

namespace TechPushCoreApi.Data.DbModels
{
    public class PushSendEntity
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<User> Users { get; set; }
        public string Title { get; set; }
    }
}
