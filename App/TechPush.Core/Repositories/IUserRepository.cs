using System;
using System.Collections.Generic;
using System.Text;
using TechPush.Core.Domain;

namespace TechPush.Core.Repositories
{
    public interface IUserRepository
    {
        UserEntity GetUser();
        void SetUser(UserEntity user);
        void RemoveAll();
    }
}
