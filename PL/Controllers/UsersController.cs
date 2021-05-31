using BL.DTO;
using Microsoft.AspNetCore.Mvc;
using BL.Services.Interfaces;
using PL.Models;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace PL.Controllers
{
    /// <summary>
    /// This controller is for users and admins.
    /// It allows you to see all materials stored in database
    /// </summary>
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _password;
        private readonly IEmailService _email;
        private readonly IRoleService _role;

        public UsersController(IUserService serv, IPasswordService passwordService, IEmailService emailService, IRoleService roleService)
        {
            _userService = serv;
            _password = passwordService;
            _email = emailService;
            _role = roleService;
        }

        /// <summary>
        /// This method fill table with users, if they exist, from business layer
        /// </summary>
        public ActionResult Index()
        {
            IEnumerable<UserDTO> userDtos = _userService.GetUsers();
            var mapperUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserDetailsViewModel>()).CreateMapper();
            var users = mapperUser.Map<IEnumerable<UserDTO>, List<UserDetailsViewModel>>(userDtos);

            if (users != null)
            {
                return View(users);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// This method return view model for UsersModel to create
        /// </summary>
        public ActionResult Create()
        {
            if (User.IsInRole("admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// This method sends new data for new user in business layer
        /// </summary>
        [HttpPost]
        public ActionResult Create(UserDetailsViewModel model)
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

                if(!_role.IsAdmin(model.Role) && !_role.IsUser(model.Role))
                {
                    ModelState.AddModelError("", "There are only this roles: 'user' and 'admin'");
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

                var userDto = new UserDTO {
                    Name       = model.Name,
                    Surname    = model.Surname,
                    Role       = model.Role,
                    Login      = model.Login,
                    Email      = model.Email, 
                    Password   = model.Password, 
                };

                _userService.SaveUser(userDto);

                TempData["message"] = string.Format("New user \"{0}\" \"{1}\" with email \"{2}\" has been saved", model.Name, model.Surname, model.Email);

                return View(model);
            }
            return View(model);
            
        }
    }
}
