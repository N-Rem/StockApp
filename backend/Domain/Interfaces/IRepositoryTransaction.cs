using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRepositoryTransaction : IRepositoryBase<Transaction>
    {
        Task<Transaction?> GetLastTransactionByBranchAsync(int branchId);
        Task<IEnumerable<Transaction?>?> GetTransacionByBranchAsync(int branchId, int productId);
    }
}
