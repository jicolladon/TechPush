using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using TechPushCoreApi.Data.Context;

using TechPushCoreApi.Services.Dtos;
using TechPushCoreApi.Services.Intefaces;

namespace TechPushCoreApi.Controllers
{
    public class UserController : Controller
    {
        readonly MyDbContext _myDbContext;

    
        private readonly IAPPServices appServices;
        private IHostingEnvironment _hostingEnvironment;

        public UserController(IAPPServices appServices, IHostingEnvironment env)
        {
            _hostingEnvironment = env;
            this.appServices = appServices;
        }

        [HttpPost]
        [Route("api/User/Login")]
        public IActionResult Login([FromBody]BaseDto user)
        {
            UserDto result = appServices.ValidateUser(user);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/User/RegisterUser")]
        public IActionResult RegisterUser([FromBody]UserDto user)
        {
            bool result = appServices.RegisterUser(user);
            return Ok(result);
        }


        [HttpPost]
        [Route("api/User/RegisterPush")]
        public IActionResult RegisterPush([FromBody]PushRegistrationDto push)
        {
            bool result = appServices.RegisterPush(push);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/User/GetAdminInfo")]
        public IActionResult GetAdminInfo([FromBody]BaseDto baseDto)
        {
            AdminInfoDto adminInfo = appServices.GetAdminInfo(baseDto);
            return Ok(adminInfo);
        }

        [HttpPost]
        [Route("api/User/GetMessages")]
        public IActionResult GetMessages([FromBody]BaseDto user)
        {
            var result = appServices.GetMessages(user);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/User/SendNotification")]
        public IActionResult SendPushNotification([FromBody]PushMessageDto message)
        {
            var result = appServices.SendNotification(message, _hostingEnvironment.ContentRootPath);
            return Ok(result);
        }
    }
}
