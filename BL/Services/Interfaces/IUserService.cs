using BL.DTO;
using System.Collections.Generic;
using System;

namespace BL.Services.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUser(Guid id);

        IEnumerable<UserDTO> GetUsers();

        public void SaveUser(UserDTO userDTO);

        bool IsPasswordSame(string password);

        bool IsEmailFree(string email);

        UserDTO GetUserLog(string email, string password);
    }
}
