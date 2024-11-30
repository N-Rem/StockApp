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

        [Required] // Agregar una funcion que cuando edite precio cree una fila en historical_prices
        public decimal Price { get; set; }

        [Required]
        public decimal MinimumQuantity { get; set; }

    }
}
