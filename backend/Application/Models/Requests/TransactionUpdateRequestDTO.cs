using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class TransactionUpdateRequestDTO
    {
        public decimal Money { get; set; } //dinero que entra o sale

        public decimal TotalQuantity { get; set; } //total de productos

        public decimal Quantity { get; set; } // cantidad que sale o entra

        public bool IsIncome { get; set; } //si es ingreso + | si es egreso -

        public int ProductId { get; set; }
    }
}
