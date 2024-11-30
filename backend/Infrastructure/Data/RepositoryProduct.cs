using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositoryProduct : RepositoryBase<Product>, IRepositoryProduct
    {
        private readonly AppDbContext _context;
        public RepositoryProduct(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Product> GetByNameAsync(string nameProduct)
        {
            var product = await _context.Products.FirstOrDefaultAsync(u => u.Name == nameProduct);
            return product;
        }
    }
}
