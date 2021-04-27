using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PL.Models;
using BL.Services.Interfaces;
using BL.DTO;
using System;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;
        IPasswordService _password;
        IEmailService _email;

        public AccountController(IUserService serv, IPasswordService password, IEmailService email)
        {
            _userService = serv;
            _password = password;
            _email = email;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserLog(model.Email, model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incrorrect login and(or) password");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                bool existErrors = false;
                if (!_userService.IsEmailFree(model.Email))
                {
                    ModelState.AddModelError("", "This email is already busy");
                    existErrors = true;
                }
                if (!_email.ValideEmail(model.Email))
                {
                    ModelState.AddModelError("", "This email is not valid");
                    existErrors = true;
                }
                if (!_password.IsPasswordStrong(model.Password))
                {
                    ModelState.AddModelError("", "Password is too weak");
                    existErrors = true;
                }

                if (existErrors)
                {
                    return View(model);
                }

                var userDto = new UserDTO
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Role = "user",
                    Login = model.Login,
                    Email = model.Email,
                    Password = model.Password,
                    DateRegist = DateTime.Now,
                };

                _userService.SaveUser(userDto);

                var user = _userService.GetUserLog(model.Email, model.Password);

                await Authenticate(user);

                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        private async Task Authenticate(UserDTO user)
        {
            var claims = new List<Claim>
            {
                //new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, Convert.ToString(user.Id)),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
