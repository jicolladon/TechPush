using System;
using System.Collections.Generic;
using System.Text;

namespace TechPushCoreApi.Services.Dtos
{
    public class AdminInfoDto
    {
        public List<string> Tags { get; set; }
        public List<int> Age { get; set; }
        public List<string> Genders { get; set; }
    }
}
