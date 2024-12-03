using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositoryHistoricalPrice : RepositoryBase<HistoricalPrice>, IRepositoryHistoricalPrice
    {
        private readonly AppDbContext _context;

        public RepositoryHistoricalPrice(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistoricalPrice>> GetByProductAsync(int idProduct)
        {
            var list = await _context.HistoricalPrices
                .Where(hp => hp.ProductId == idProduct).AsNoTracking().ToListAsync();
            return list;
        }
    }
}
