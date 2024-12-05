using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TransactionDTO
    {
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
        public DateTime Created { get; set; }

        [Required]
        public int ProductId { get; set; }

        public static TransactionDTO Create(Transaction t)
        {
            var dto = new TransactionDTO();
            dto.Id = t.Id;
            dto.Money = t.Money;
            dto.TotalQuantity = t.TotalQuantity;
            dto.Quantity = t.Quantity;
            dto.IsIncome = t.IsIncome;
            dto.Created = t.Created;
            dto.ProductId = t.ProductId;

            return dto;
        }

        public static List<TransactionDTO?> CreateList(IEnumerable<Transaction> transactions)
        {
            List<TransactionDTO> listDto = [];

            foreach (var t in transactions)
            {
                listDto.Add(Create(t));
            }

            return listDto;
        }

    }
}
