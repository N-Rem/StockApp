using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BranchTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(TransactionId))]
        public int TransactionId { get; set; }

        [Required]
        [ForeignKey(nameof(BranchId))]
        public int BranchId { get; set; }
    }
}
