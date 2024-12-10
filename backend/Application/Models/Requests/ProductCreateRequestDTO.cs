using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class ProductCreateRequestDTO
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required] 
        public decimal Price { get; set; }

        [Required]
        public decimal MinimumQuantity { get; set; }

        [Required] //supplierproduct tabla intermedia
        public int SupplierId { get; set; }

    }
}
