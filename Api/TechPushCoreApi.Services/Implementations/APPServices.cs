using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using TechPushCoreApi.Data.DbModels;
using TechPushCoreApi.Data.Repositories.Interfaces;
using TechPushCoreApi.Services.Dtos;
using TechPushCoreApi.Services.Entities;
using TechPushCoreApi.Services.Intefaces;

namespace TechPushCoreApi.Services.Implementations
{
    public class APPServices : IAPPServices
    {
        private readonly IUserRepository userRepository;
        private readonly IRegPushRepository regPushRepository;
        private readonly IMessageRepository messageRepository;
        private readonly IEntityMapper entityMapper;
        private IOptions<ServiceSettings> _settings;
        private IPushService _pushService;
        private ICipher _cipher;

        public APPServices(IUserRepository userRepository,
                            IRegPushRepository regPushRepository,
                            IMessageRepository messageRepository,
                            IOptions<ServiceSettings> settings, 
                            IPushService pushService,
                            ICipher cipher,
                            IEntityMapper entityMapper)
        {
            _cipher = cipher;
            _pushService = pushService;
            _settings = settings;
            this.userRepository = userRepository;
            this.regPushRepository = regPushRepository;
            this.messageRepository = messageRepository;
            this.entityMapper = entityMapper;
        }
        public UserDto ValidateUser(BaseDto user)
        {
            var result = userRepository.GetUser(user.Username,_cipher.Encrypt(user.Password, _settings.Value.AppKey));

            if (result == null)
            {
                throw new UnauthorizedAccessException("Usuario y contraseña no validos");
            }
            return entityMapper.Map<User, UserDto>(result);
        }


        public bool RegisterPush(PushRegistrationDto push)
        {
            var result = false;
            if (IsValidPush(push) && ValidateLoginUser(push))
            {
                var pushDB = regPushRepository.GetByDeviceId(push.DeviceId);
                if (pushDB == null)
                {
                    var newPush = entityMapper.Map<PushRegistrationDto, PushRegistration>(push);
                    newPush.Id = Guid.NewGuid();
                    newPush.User = userRepository.GetUser(push.Username);
                    newPush.CreationDate = DateTime.Now;
                    newPush.UpdateDate = DateTime.Now;
                    newPush.Tags = string.Join('|', push.Tags.ToArray());
                    result = regPushRepository.AddPushReg(newPush);
                }
                else
                {
                    pushDB.User = userRepository.GetUser(push.Username);
                    pushDB.UpdateDate = DateTime.Now;
                    pushDB.Token = push.DeviceToken;
                    pushDB.Tags = string.Join('|', push.Tags.ToArray());
                    result = regPushRepository.Update(pushDB);
                }
            }
            return result;
        }

        private bool IsValidPush(PushRegistrationDto push)
        {
            return push != null
                            && !string.IsNullOrEmpty(push.DeviceToken)
                            && !string.IsNullOrEmpty(push.DeviceId);
        }

        public bool RegisterUser(UserDto user)
        {
            User userDB = userRepository.GetUser(user.Username);
            var result = false;
            if (userDB == null && IsValid(user))
            {
                user.Gender = user.Gender ?? string.Empty;
                var newUser = entityMapper.Map<UserDto, User>(user);
                newUser.UserPassword = _cipher.Encrypt(user.Password, _settings.Value.AppKey);
                newUser.Id = Guid.NewGuid();

                result = userRepository.AddUser(newUser);
            }
            return result;
        }

        private bool IsValid(UserDto user)
        {
            return user != null
                   && !string.IsNullOrEmpty(user.Username)
                   && !string.IsNullOrEmpty(user.Password);

        }

        public IEnumerable<MessageDto> GetMessages(BaseDto user)
        {
            IEnumerable<MessageDto> result = new List<MessageDto>();
            if (ValidateLoginUser(user))
            {
                var messages = messageRepository.GetMessagesOfUser(user.Username);
                result = entityMapper.Map<IEnumerable<PushMessage>, IEnumerable<MessageDto>>(messages);
            }
            return result;
        }

        private bool ValidateLoginUser(BaseDto user)
        {
            return userRepository.ExistUser(user.Username, _cipher.Encrypt(user.Password,_settings.Value.AppKey));
        }

        public AdminInfoDto GetAdminInfo(BaseDto baseDto)
        {
            var result = new AdminInfoDto();
            var user = ValidateUser(baseDto);
            if (user != null && user.IsAdmin)
            {
                result.Tags = new List<string>(regPushRepository.GetTags());
                result.Age = new List<int>(userRepository.GetAges());
                result.Genders = new List<string>(userRepository.GetGenders());
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
            return result;
        }

        public bool SendNotification(PushMessageDto notification, string rootPath)
        {
            IEnumerable<User> users = new List<User>();
            if (notification.SelectRandom)
            {
                users = userRepository.SelectRandomUsers(_settings.Value.RandomQt);
            }
            else
            {
                users = userRepository.GetUsersSubscribedTo(notification.Tag, notification.Genders, notification.Ages);
            }
            foreach (var user in users)
            {
                var message = new PushMessage()
                {
                    User = user,
                    Tag = notification.Title,
                    Date = notification.Date,
                    Description = notification.Description,
                    Id = Guid.NewGuid(),
                    Image = notification.Image,
                };
                messageRepository.InsertMessage(message);
            }

            var saved = messageRepository.SaveChanges();
            if (saved)
            {
                _pushService.SendNotification(new PushSendEntity()
                {
                    Message = notification.Description,
                    Users = users,
                    Date = notification.Date,
                    Title = notification.Title,

                }, rootPath);
            }

            return true;
        }
    }
}
