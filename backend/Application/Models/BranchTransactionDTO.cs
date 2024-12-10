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
    public class BranchTransactionDTO
    {
        public int Id { get; set; }

        [Required]
        public int TransactionId { get; set; }

        [Required]
        public int BranchId { get; set; }

        public static BranchTransactionDTO Create(BranchTransaction bt)
        {
            var dto = new BranchTransactionDTO();
            dto.Id = bt.Id;
            dto.TransactionId = bt.TransactionId;
            dto.BranchId = bt.BranchId;

            return dto;
        }

        public static List<BranchTransactionDTO?> CreateList(IEnumerable<BranchTransaction> branchTransactions)
        {
            List<BranchTransactionDTO> listDto = [];

            foreach (var bt in branchTransactions)
            {
                listDto.Add(Create(bt));
            }

            return listDto;
        }
    }
}
