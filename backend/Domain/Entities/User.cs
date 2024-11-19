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
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]{6, }$", ErrorMessage = "The password must be 6 or more characters, numbers and letter.")]
        public string Password { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public bool IsOwner { get; set; }

        [ForeignKey("OwnerId")]
        public int? OwnerId { get; set; }

    }
}
