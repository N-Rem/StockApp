using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class BranchTransactionUpdateRequestDTO
    {
        [Required]
        public int TransactionId { get; set; }

        [Required]
        public int BranchId { get; set; }
    }
}
