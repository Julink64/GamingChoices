using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewGamingChoices.Data;
using NewGamingChoices.Models;
using NewGamingChoices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewGamingChoices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private ApplicationDbContext _db;

        public UserController(ILogger<UserController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("[action]")]
        public ApplicationUser getUser(string useremail)
        {
            CustomUserService userService = new CustomUserService(_db);
            return userService.GetUser(useremail);
        }

        [HttpPost("[action]")]
        public void updateUser(ApplicationUser user)
        {
            CustomUserService userService = new CustomUserService(_db);
            userService.UpdateUser(user);
        }
    }
}
