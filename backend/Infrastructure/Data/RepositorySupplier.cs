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
    public class RepositorySupplier : RepositoryBase<Supplier>, IRepositorySupplier
    {
        private readonly AppDbContext _context;

        public RepositorySupplier(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Supplier> GetByNameAsync(string name)
        {
            var listObj = await _context.Suppliers.FirstOrDefaultAsync(s=>s.Name == name);
            return listObj;
        }

    }
}
