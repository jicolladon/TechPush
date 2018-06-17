using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TechPushCoreApi.Data.Context;
using TechPushCoreApi.Data.DbModels;
using TechPushCoreApi.Data.Repositories.Interfaces;

namespace TechPushCoreApi.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext context;

        public DbSet<User> DBSet { get; }

        public UserRepository(MyDbContext context)
        {
            this.context = context;
            this.DBSet = context.Set<User>();
        }


        public bool ExistUser(string username, string password)
        {
            return DBSet.Any(x => x.UserEmail == username && x.UserPassword == password);
        }


        public User GetUser(string username)
        {
            return DBSet.SingleOrDefault(x => x.UserEmail == username);
        }



        public bool AddUser(User newUser)
        {
            var result = DBSet.Add(newUser);
            return context.SaveChanges() > 0;
        }

        public User GetUser(string username, string password)
        {
            return DBSet.SingleOrDefault(x => x.UserEmail == username && x.UserPassword == password);
        }

        public IEnumerable<int> GetAges()
        {
            return DBSet.Where(x=>x.Age > 0).Select(x => x.Age).Distinct();
        }

        public IEnumerable<string> GetGenders()
        {
            return DBSet.Where(x=>!string.IsNullOrEmpty(x.Gender)).Select(x => x.Gender).Distinct();
        }

        public IEnumerable<User> GetUsersSubscribedTo(List<string> tags, List<string> genders, List<int> ages)
        {
            var usersToReturn = new List<User>();
            foreach (var tag in tags)
            {
                var user = DBSet.Include(x => x.PushRegistration)
                    .Where(x => x.PushRegistration != null && x.PushRegistration.Any(y => y.Tags.Contains(tag)));
                usersToReturn.AddRange(user);
            }

            usersToReturn = usersToReturn.Distinct().ToList();

            if (ages.Any())
            {
                usersToReturn = usersToReturn.Where(x => ages.Contains(x.Age)).ToList();
            }

            if (genders.Any())
            {
                usersToReturn = usersToReturn.Where(x => genders.Contains(x.Gender)).ToList();
            }

            return usersToReturn;

        }

        public IEnumerable<User> SelectRandomUsers(int randomQt)
        {
            var users = DBSet.Include(x => x.PushRegistration).ToList();
            return users.OrderBy(x => Guid.NewGuid()).Take(Math.Min(users.Count(),randomQt));
        }
    }
}
