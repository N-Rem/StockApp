using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class TransactionCreateRequestDTO
    {

        [Required]
        public decimal Money { get; set; } //dinero que entra o sale

        //total de productos

        [Required]
        public decimal Quantity { get; set; } // cantidad que sale o entra

        [Required]
        public bool IsIncome { get; set; } //si es ingreso + money(+) quantity(-) | si es egreso - money(-) quantity(+)

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int BranchId { get; set; }

    }
}
