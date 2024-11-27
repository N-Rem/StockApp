using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UserUpdateRequestDTO
    {
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public string Email { get; set; }

       [RegularExpression(@"^[a-zA-Z0-9]{6, }$", ErrorMessage = "The password must be 6 or more characters, numbers and letter.")]
        public string Password { get; set; }

        [Phone(ErrorMessage = "The phone number is not valid.")]
        public string Phone { get; set; }
    }
}
