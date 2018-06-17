using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechPushCoreApi.Data.Context;
using TechPushCoreApi.Data.DbModels;
using TechPushCoreApi.Data.Repositories.Interfaces;

namespace TechPushCoreApi.Data.Repositories.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        public DbSet<PushMessage> DBSet { get; }

        readonly MyDbContext _context;
        public MessageRepository(MyDbContext context)
        {
            _context = context;
            this.DBSet = context.Set<PushMessage>();

        }

        public IEnumerable<PushMessage> GetMessagesOfUser(string username)
        {
            return DBSet.Where(x => x.User.UserEmail == username).ToList();
        }

        public void InsertMessage(PushMessage message)
        {
            DBSet.Add(message);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
