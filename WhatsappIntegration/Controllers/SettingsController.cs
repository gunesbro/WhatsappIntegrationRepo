using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WhatsappIntegration.Entity.Concrete;

namespace WhatsappIntegration.WebUI.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<UserAgent> userManager;
        private readonly SignInManager<UserAgent> signInManager;
        public SettingsController(UserManager<UserAgent> _userManager, SignInManager<UserAgent> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUserAgent(UserAgent userAgent,string password)
        {
            //UserAgent agent = new UserAgent()
            //{
            //    Name = "Burak",
            //    Surname = "Gunes",
            //    CompanyId = 1,
            //    UserName = "gunesbro",
            //    Email = "gunes.burak77@gmail.com",
            //    PhoneNumber = "5555555555",
            //};
            var result = await userManager.CreateAsync(userAgent, password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(userAgent);
            }
            return View();
        }
    }
}