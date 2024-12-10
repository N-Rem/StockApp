using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositorySupplierProduct : RepositoryBase<SupplierProduct>, IRepositorySupplierProduct
    {
        private readonly AppDbContext _context;
        public RepositorySupplierProduct(AppDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
