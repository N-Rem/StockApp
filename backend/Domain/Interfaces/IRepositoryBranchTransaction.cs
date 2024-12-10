using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryBranchTransaction : IRepositoryBase<BranchTransaction>
    {
        Task<IEnumerable<BranchTransaction>> GetTransactionsByBranchAsync(int branchId);
        Task<BranchTransaction?> GetByTransaction(int transactionId);

    }
}
