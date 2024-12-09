using Domain.Enums;
using Newtonsoft.Json.Converters; //combierte el numero del enum en su string correspondiente. 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UserCreateRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]{6,}$", ErrorMessage = "The password must be 6 or more characters, numbers and letter.")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^\d{3} \d{3} \d{4}$", ErrorMessage = "The phone number must be in the format xxx xxx xxxx.")]
        public string Phone { get; set; }

        public int OwnerId { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType Type { get; set; }
    }
}
