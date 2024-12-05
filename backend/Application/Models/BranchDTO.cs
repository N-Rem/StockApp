using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class BranchDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Addres { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]

        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "The phone number must be in the format xxx-xxx-xxxx.")]
        public string Tel { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public static BranchDTO Create(Branch b)
        {
            var dto = new BranchDTO();
            dto.Id = b.Id;
            dto.Name = b.Name;
            dto.Addres = b.Addres;
            dto.Description = b.Description;
            dto.Tel = b.Tel;
            dto.OwnerId = b.OwnerId;
            return dto;
        }

        public static List<BranchDTO?> CreateList(IEnumerable<Branch> Branches)
        {
            List<BranchDTO?> listDto = [];

            foreach (var b in Branches)
            {
                listDto.Add(Create(b));
            }
            return listDto;
        }
    }
}
