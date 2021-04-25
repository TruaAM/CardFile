using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Core.Enums;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;

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

        public UserDTO GetUser(Guid id)
        {
            var user = _unitOfWork.Users.Get(id);

            return new UserDTO { Email = user.Email, Password = user.Password };
        }

        public bool IsPasswordSame(string password)
        {
            IEnumerable<UserDTO> userDtos = GetUsers();
            foreach (UserDTO userDto in userDtos)
            {
                if (userDto.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsEmailFree(string email)
        {
            IEnumerable<UserDTO> userDtos = GetUsers();
            foreach (UserDTO userDto in userDtos)
            {
                if (userDto.Email == email)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(_unitOfWork.Users.GetAll());
        }

        public void SaveUser(UserDTO userDTO)
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
                RoleName = userDTO.RoleName,
                Email = userDTO.Email,
                Password = _password.GetHashString(userDTO.Password),
            };

            _unitOfWork.Users.Create(user);

            _unitOfWork.Save();
        }
    }
}
