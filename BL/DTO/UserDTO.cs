using System;

namespace BL.DTO
{
    public class UserDTO
    {
        public Guid Id { set; get; }

        public string Name { set; get; }

        public string Surname { set; get; }

        public string Role { set; get; }

        public string Login { set; get; }

        public string Email { set; get; }

        public string Password { set; get; }

        public DateTime DateRegist { get; set; }
    }
}
