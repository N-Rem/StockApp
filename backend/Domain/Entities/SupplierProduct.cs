using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SupplierProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(SupplierId))]
        public int SupplierId { get; set; }

        [Required]
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }

    }
}
