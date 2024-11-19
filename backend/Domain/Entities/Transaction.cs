using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Money { get; set; } //dinero que entra o sale

        [Required]
        public decimal TotalQuantity { get; set; } //total de productos

        [Required]
        public decimal Quantity { get; set; } // cantidad que sale o entra

        [Required]
        public bool IsIncome { get; set; } //si es ingreso + | si es egreso -

        [Required]
        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
    }
}
