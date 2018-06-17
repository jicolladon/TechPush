using System;
using System.Collections.Generic;
using TechPushCoreApi.Data.DbModels;

namespace TechPushCoreApi.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool ExistUser(string username, string password);
        User GetUser(string username);
        bool AddUser(User newUser);
        User GetUser(string username, string password);
        IEnumerable<int> GetAges();
        IEnumerable<string> GetGenders();
        IEnumerable<User> GetUsersSubscribedTo(List<string> tags, List<string> genders, List<int> ages);
        IEnumerable<User> SelectRandomUsers(int randomQt);
    }
}
