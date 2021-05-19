using AutoMapper;
using BL.DTO;
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

        public UserService()
        {    
            _unitOfWork = new UnitOfWork();
            _password = new PasswordService();
            _email = new EmailService();
        }

        public Task<UserDTO> GetUser(Guid id)
        {
            var user = _unitOfWork.Users.GetAsync(id).Result;

            return Task.FromResult(new UserDTO
            {
                Name = user.Name,
                Surname = user.Surname,
                Role = user.Role,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                DateRegist = user.DateRegist
            }
            );
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
            if (userDto != null)
            {
                return Task.FromResult(userDto);
            }
            return Task.FromResult(userDto);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(_unitOfWork.Users.GetAll());
        }     

        public Task SaveUser(UserDTO userDTO)
        {
            if (!_email.ValideEmail(userDTO.Email))
            {
                throw new Exception("Invalide Email");
            }
            if (_password.CheckPasswordStrength(userDTO.Password) < PasswordStrength.Medium)
            {
                throw new Exception("Pass not strong enough");
            }

            User user = new User
            {
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Role = userDTO.Role,
                Login = userDTO.Login,
                Email = userDTO.Email,
                Password = _password.GetHashString(userDTO.Password),
                DateRegist = userDTO.DateRegist
            };

            _unitOfWork.Users.CreateAsync(user);

            return Task.FromResult(_unitOfWork.SaveAsync());
        }
    }
}
