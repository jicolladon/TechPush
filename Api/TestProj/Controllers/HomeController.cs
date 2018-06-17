using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechPushCoreApi.Data.Context;
using TechPushCoreApi.Models;

namespace TechPushCoreApi.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        [HttpGet]
        [Route("api/Home")]
        public IActionResult Index()
        {
            return Ok("Working");
        }

    
    }
}
