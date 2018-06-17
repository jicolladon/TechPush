using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechPush.Core.Data;
using TechPush.Core.Domain;
using TechPush.Core.Repositories;

namespace TechPush.Data.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(IConnectionWrapper connectionWrapper) : base(connectionWrapper)
        {
        }

        public UserEntity GetUser()
        {
            var user = GetAll();
            return user.FirstOrDefault();
        }

        public void SetUser(UserEntity user)
        {
            RemoveAll();
            this.Insert(user);
        }

        public void RemoveAll()
        {
            var users = GetAll();

            foreach (var usr in users)
            {
                Remove(usr);
            }
        }
    }
}