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
    public class RepositoryTransaction : RepositoryBase<Transaction> , IRepositoryTransaction
    {
        private readonly AppDbContext _context;
        private readonly IRepositoryBranchTransaction _repositoryBranchTransaction;
        public RepositoryTransaction(AppDbContext context, IRepositoryBranchTransaction repositoryBranchTransaction) : base(context)
        {
            _context = context;
            _repositoryBranchTransaction = repositoryBranchTransaction;
        }

        //Traerme el ulitmo registro para calcular en servicio
        public async Task<Transaction?> GetLastTransactionByBranchAsync(int branchId)
        {
            var listBranchTransaction = await _repositoryBranchTransaction
                .GetTransactionsByBranchAsync(branchId);
            if (listBranchTransaction == null || !listBranchTransaction.Any())
            {
                return null;
            }

            var transactionIds = listBranchTransaction.Select(bt => bt.TransactionId).ToList();
            
            var lastTransaction = await _context.Transactions
                .Where(t => transactionIds.Contains(t.Id))
                .OrderByDescending(t => t.Id).FirstOrDefaultAsync();

            return lastTransaction;
        } 
        

        //devuelve una lista de todas las transaciones de la branch y producto marcado.
        public async Task<IEnumerable<Transaction?>?> GetTransacionByBranchAsync(int branchId, int productId)
        {
            var listBranchTransaction = await _repositoryBranchTransaction
                .GetTransactionsByBranchAsync(branchId);

            if (listBranchTransaction == null || !listBranchTransaction.Any())
            {
                return Enumerable.Empty<Transaction>(); 
            }
            var transactionIds = listBranchTransaction.Select(bt => bt.TransactionId).ToList();

            var allTransactions = await _context.Transactions
                .Where(t => transactionIds
                .Contains(t.Id))
                .ToListAsync();

            var transactions = allTransactions.Where(t => t.ProductId == productId).ToList();

            if (transactions == null || !transactions.Any())
            {
                return Enumerable.Empty<Transaction>();
            }

            return transactions.Any() ? (IEnumerable<Transaction?>?)transactions : null; 
        }


    }
}
