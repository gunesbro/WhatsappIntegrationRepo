using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WhatsappIntegration.Entity.Concrete;
using WhatsappIntegration.WebUI.Controllers;
using WhatsappIntegration.WebUI.Models;

namespace WhatsappIntegration.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserAgent> userManager;
        private readonly SignInManager<UserAgent> signInManager;
        public AccountController(UserManager<UserAgent> _userManager, SignInManager<UserAgent> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            TempData["returnUrl"] = ReturnUrl;
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                await signInManager.SignOutAsync();
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    string returnUrl = "";
                    if (TempData["returnUrl"] != null)
                    {
                        returnUrl = TempData["returnUrl"].ToString();
                    }

                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username or Password incorrect!");
                    return View(model);
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}