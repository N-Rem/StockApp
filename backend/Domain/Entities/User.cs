using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]{6,}$", ErrorMessage = "The password must be 6 or more characters, numbers and letter.")]
        public string Password { get; set; }

        [Required]
        [Phone(ErrorMessage = "The phone number is not valid.")]
        public string Phone { get; set; }

        [ForeignKey("OwnerId")]
        public int? OwnerId { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public UserType Type { get; set; }

    }
}
