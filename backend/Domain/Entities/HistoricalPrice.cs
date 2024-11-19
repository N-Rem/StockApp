using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HistoricalPrice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public DateTime PriceDate { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }

    }
}
