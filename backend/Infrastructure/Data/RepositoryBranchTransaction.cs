using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositoryBranchTransaction : RepositoryBase<BranchTransaction>, IRepositoryBranchTransaction
    {
        private readonly AppDbContext _context;

        public RepositoryBranchTransaction(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BranchTransaction>> GetTransactionsByBranchAsync(int branchId)
        {
            var list = await _context.BranchTransactions
                .Where(bt => bt.BranchId == branchId)
                .ToListAsync();

            return list ?? Enumerable.Empty<BranchTransaction>(); 
        }

        public async Task<BranchTransaction?> GetByTransaction(int transactionId)
        {
            var bt = await _context.BranchTransactions.FindAsync(transactionId);
            return bt;
        }
    }
}
