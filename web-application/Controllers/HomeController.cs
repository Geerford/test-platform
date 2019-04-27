using Application.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace web_application.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IUserService service;

        public HomeController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            List<User> x = service.GetAll().ToList();
            return View();
        }
    }
}