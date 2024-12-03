using Domain.Entities;
using Domain.Interfaces;
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
        public RepositoryTransaction(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
