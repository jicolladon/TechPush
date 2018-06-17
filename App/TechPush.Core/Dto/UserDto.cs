using System;
using System.Collections.Generic;
using System.Text;

namespace TechPush.Core.Dto
{
    public class UserDto : BaseDto
    {

        public int Age { get; set; }
        public string Gender { get; set; }
        public bool RememberMe { get; set; }
        public bool IsAdmin { get; set; }
    }
}
