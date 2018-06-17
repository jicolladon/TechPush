using System;
using System.Collections.Generic;
using System.Text;

namespace TechPush.Core.Domain
{
    public class UserEntity : BaseEntity<Guid>
    {
       
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool IsAdmin { get; set; }

    }
}
