using Core.Enums;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using BL.Services.Interfaces;
using System.Text;

namespace BL.Services
{
    /// <summary>
    /// Service to check roles of current users
    /// </summary>
    public class RoleService : IRoleService
    {
        /// <summary>
        /// Method that returns current role
        /// </summary>
        public Role RoleSpecificator(string role)
        {
            int score = 0;
            if(role == "admin")
            {
                score = 2;
            }
            if (role == "user")
            {
                score = 1;
            }
            Role result;
            switch (score)
            {
                case 0: result = Role.Guest; break;
                case 1: result = Role.User; break;
                case 2: result = Role.Admin; break;
                default: result = Role.Guest; break;
            }
            return result;
        }

        /// <summary>
        /// Check if current role is admin
        /// </summary>
        public bool IsAdmin(string role)
        {
            if (RoleSpecificator(role) != Role.Admin)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check if current role is user
        /// </summary>
        public bool IsUser(string role)
        {
            if (RoleSpecificator(role) != Role.User)
            {
                return false;
            }
            return true;
        }
    }
}
