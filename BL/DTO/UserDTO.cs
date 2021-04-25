using System;

namespace BL.DTO
{
    public class UserDTO
    {
        public Guid Id { set; get; }

        public string RoleName { set; get; }

        public string Email { set; get; }

        public string Login { get; set; }

        public string Password { set; get; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public DateTime DateRegist { get; set; }
    }
}
