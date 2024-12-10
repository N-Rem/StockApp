using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class BranchUpdateRequestDTO
    {
        public string Name { get; set; }

        public string Addres { get; set; }

        public string Description { get; set; }


        [RegularExpression(@"^\d{3} \d{3} \d{4}$", ErrorMessage = "The phone number must be in the format xxx xxx xxxx.")]
        public string Tel { get; set; }

    }
}
