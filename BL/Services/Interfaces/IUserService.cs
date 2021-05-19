using BL.DTO;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(Guid id);

        IEnumerable<UserDTO> GetUsers();

        public Task SaveUser(UserDTO userDTO);

        bool IsEmailFree(string email);

        Task<UserDTO> GetUserLog(string email, string password);
    }
}
