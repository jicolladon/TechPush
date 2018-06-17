using System;
using System.Collections.Generic;
using TechPushCoreApi.Data.DbModels;

namespace TechPushCoreApi.Data.Repositories.Interfaces
{
    public interface IRegPushRepository
    {
        IEnumerable<PushRegistration> GetAll();
        IEnumerable<PushRegistration> GetIosReg();
        IEnumerable<PushRegistration> GetDroidReg();
        IEnumerable<PushRegistration> GetRegFromUserId(Guid userId);
        IEnumerable<PushRegistration> GetRegFromUsersId(IEnumerable<Guid> userIds);
        PushRegistration GetByDeviceId(string deviceId);
        bool AddPushReg(PushRegistration pushReg);
        bool Update(PushRegistration pushDB);
        IEnumerable<string> GetTags();
    }
}
