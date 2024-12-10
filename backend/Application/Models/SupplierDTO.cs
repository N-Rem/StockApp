using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SupplierDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Phone(ErrorMessage = "The phone number is not valid.")]
        public string Tel { get; set; }

        public static SupplierDTO Create(Supplier s)
        {
            var dto = new SupplierDTO();
            dto.Id = s.Id;
            dto.Name = s.Name;
            dto.Description = s.Description;
            dto.Address = s.Address;    
            dto.Tel = s.Tel;
            return dto;
        }

        public static List<SupplierDTO?> CreateList(IEnumerable<Supplier> suppliers)
        {
            List<SupplierDTO?> listDto = [];

            foreach (var s in suppliers)
            {
                listDto.Add(Create(s));
            }
            return listDto;
        }
    }
}
