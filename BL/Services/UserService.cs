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

        public Task<UserDTO> GetByIdAsync(Guid id)
        {
            User user = _unitOfWork.Users.GetAsync(id).Result;
            return Task.FromResult(_automapper.Map<User, UserDTO>(user));
        }

        public bool IsEmailFree(string email)
        {
            IEnumerable<UserDTO> userDtos = GetUsers();
            UserDTO userDto = userDtos.Where(user => user.Email == email).FirstOrDefault();
            if (userDto != null)
            {
                return false;
            }
            return true;
        }       

        public Task<UserDTO> GetUserLog(string email, string password)
        {
            IEnumerable<UserDTO> userDtos = GetUsers();
            UserDTO userDto = userDtos.Where(user => user.Email == email && user.Password == _password.GetHashString(password)).FirstOrDefault();
            return Task.FromResult(userDto);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<User> allUsers = _unitOfWork.Users.GetAll();
            return _automapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(allUsers);
        }     

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

            User user = _automapper.Map<UserDTO, User>(userDTO);
            _unitOfWork.Users.CreateAsync(user);
            return Task.FromResult(_unitOfWork.SaveAsync());
        }
    }
}
