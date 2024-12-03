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
        public int Tel { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public static BranchDTO Create(Branch b)
        {
            var dto = new BranchDTO();
            dto.Id = b.Id;
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
