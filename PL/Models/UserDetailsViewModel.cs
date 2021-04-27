using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class UserDetailsViewModel
    {
        [Required(ErrorMessage = "Name is not specified")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is not specified")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Role is not specified")]
        public string Role { set; get; }

        [Required(ErrorMessage = "Login is not specified")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email is not specified")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is not specified")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password was not repeat correctly")]
        public string ConfirmPassword { get; set; }
    }
}
