using AutoMapper;
using BL.DTO;
using BL.Exceptions;
using BL.Services.Interfaces;
using Core.Enums;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    /// <summary>
    /// Service for manipulation with data of users
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IPasswordService _password;

        private readonly IEmailService _email;

        private readonly Mapper _automapper;

        public UserService()
        {    
            _unitOfWork = new UnitOfWork();
            _password = new PasswordService();
            _email = new EmailService();

            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _automapper = new Mapper(configuration);
        }

        /// <summary>
        /// Method to get user by id from datalayer
        /// </summary>
        public Task<UserDTO> GetByIdAsync(Guid id)
        {
            User user = _unitOfWork.Users.GetAsync(id).Result;
            return Task.FromResult(_automapper.Map<User, UserDTO>(user));
        }

        /// <summary>
        /// Method to check if there users with the same given email
        /// </summary>
        public bool IsEmailFree(string email)
        {
            email = email.Trim();
            IEnumerable<UserDTO> userDtos = GetUsers();
            UserDTO userDto = userDtos.Where(user => user.Email == email).FirstOrDefault();
            if (userDto != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Method has to find user with same email and hashed password
        /// </summary>
        public Task<UserDTO> GetUserLog(string email, string password)
        {
            IEnumerable<UserDTO> userDtos = GetUsers();
            UserDTO userDto = userDtos.Where(user => user.Email == email && user.Password == _password.GetHashString(password)).FirstOrDefault();
            return Task.FromResult(userDto);
        }

        /// <summary>
        /// Method to get users from datalayer
        /// </summary>
        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<User> allUsers = _unitOfWork.Users.GetAll();
            return _automapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(allUsers);
        }

        /// <summary>
        /// Method to transfer new user in datalayer
        /// </summary>
        public Task SaveUser(UserDTO userDTO)
        {
            if (!_email.ValideEmail(userDTO.Email))
            {
                throw new UserArgumentException("Invalide Email");
            }
            if (_password.CheckPasswordStrength(userDTO.Password) < PasswordStrength.Medium)
            {
                throw new UserArgumentException("Password is not strong enough");
            }

            userDTO.Password = _password.GetHashString(userDTO.Password);

            User user = _automapper.Map<UserDTO, User>(userDTO);
            _unitOfWork.Users.CreateAsync(user);
            return Task.FromResult(_unitOfWork.SaveAsync());
        }
    }
}
