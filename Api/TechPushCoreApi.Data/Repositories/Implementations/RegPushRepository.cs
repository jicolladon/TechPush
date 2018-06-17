using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TechPushCoreApi.Data.Context;
using TechPushCoreApi.Data.DbModels;
using TechPushCoreApi.Data.Repositories.Interfaces;

namespace TechPushCoreApi.Data.Repositories.Implementations
{
    public class RegPushRepository : IRegPushRepository
    {
        public DbSet<PushRegistration> DBSet { get; }

        readonly MyDbContext _context;
        public RegPushRepository(MyDbContext context)
        {
            _context = context;
            this.DBSet = context.Set<PushRegistration>();

        }

        public bool AddPushReg(PushRegistration pushReg)
        {
            var result = DBSet.Add(pushReg);
            return _context.SaveChanges() > 0;
            
        }

        public IEnumerable<PushRegistration> GetAll()
        {
            return _context.PushRegistration.ToList();
        }

        public PushRegistration GetByDeviceId(string deviceId)
        {
            return _context.PushRegistration.SingleOrDefault(p => p.DeviceId == deviceId);
        }

        public IEnumerable<PushRegistration> GetDroidReg()
        {
            return _context.PushRegistration.Where(p => p.PlatformType == "AnDroid");
        }

        public IEnumerable<PushRegistration> GetIosReg()
        {
            return _context.PushRegistration.Where(p => p.PlatformType == "iOS");
        }

        public IEnumerable<PushRegistration> GetRegFromUserId(Guid userId)
        {
            return _context.PushRegistration.Where(p => p.User.Id == userId);
        }

        public IEnumerable<PushRegistration> GetRegFromUsersId(IEnumerable<Guid> userIds)
        {
            return _context.PushRegistration.Where(p => userIds.Contains(p.User.Id));
        }

        public bool Update(PushRegistration pushDB)
        {
            var result = DBSet.Update(pushDB);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<string> GetTags()
        {
            var tagsDB = DBSet.Select(x => x.Tags).ToList();
            var tags = tagsDB.SelectMany(x => x.Split('|').ToList());
            return tags.Distinct();
        }
    }
}
