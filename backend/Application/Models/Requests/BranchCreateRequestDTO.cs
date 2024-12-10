using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class BranchCreateRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Addres { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]

        [RegularExpression(@"^\d{3} \d{3} \d{4}$", ErrorMessage = "The phone number must be in the format xxx xxx xxxx.")]
        public string Tel { get; set; }

        [Required]
        public int OwnerId { get; set; }
    }
}
