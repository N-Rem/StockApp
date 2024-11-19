using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Branch
    {
        [Key]
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
        [ForeignKey(nameof(OwnerId))]
        public int OwnerId { get; set; }
    }
}
