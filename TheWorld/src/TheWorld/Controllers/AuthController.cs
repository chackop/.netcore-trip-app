using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Model;
using TheWorld.ViewModels;

namespace TheWorld.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _signinmanager;

        public AuthController(SignInManager<WorldUser> signinmanager)
        {
            _signinmanager = signinmanager;
        }
        public  IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signinmanager.PasswordSignInAsync(vm.Username, vm.Password, true, false);
                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        return RedirectToAction(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("","Username or password incorrect");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signinmanager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }

    }
}
