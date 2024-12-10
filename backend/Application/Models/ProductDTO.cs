using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required] // Agregar una funcion que cuando edite precio cree una fila en historical_prices
        public decimal Price { get; set; }

        [Required]
        public decimal MinimumQuantity { get; set; }


        public static ProductDTO Create(Product p)
        {
            var dto = new ProductDTO();
            dto.Id = p.Id;
            dto.Name = p.Name;
            dto.Description = p.Description;
            dto.Price = p.Price;
            dto.MinimumQuantity = p.MinimumQuantity;
            
            return dto;
        }

        public static List<ProductDTO?> CreateList(IEnumerable<Product> products)
        {
            List <ProductDTO>listDto = [];

            foreach (var p in products) 
            {
                listDto.Add(Create(p));
            }

            return listDto;
        }
    }
}
