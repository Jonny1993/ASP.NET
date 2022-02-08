using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Social.Areas.Identity.Data;
using Social.Models;
using Social.BusinessObjects;
using Social.Data;

namespace Social.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserHandler userHandler;
        // Summary:
        //     Initializes a new instance with dependency injection for userHandler object as well as logger
        //
        // Parameters:
        //   logger
        //   myUserManager
        //   socialIDContext

        public HomeController(ILogger<HomeController> logger, UserManager<SocialUser> myUserManager, SocialIDContext socialIDContext)
        {
            _logger = logger;
            userHandler = new UserHandler(myUserManager,  socialIDContext);
        }

        public  IActionResult Index()
        {
            var userVM = userHandler.getUserVM(User);

            return View(userVM);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}