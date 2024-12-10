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
        public async Task UpdateProductPriceAsync(Product updateProduct, decimal newPrice)
        {
            // Solo el precio
            updateProduct.Price = newPrice;

            // Solo el campo Price debe ser modificado
            _context.Entry(updateProduct).Property(p => p.Price).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductExceptPriceAsync(Product updatedProduct)
        {
            // Especificar qué campos se deben marcar como modificados
            _context.Entry(updatedProduct).Property(p => p.Name).IsModified = true;
            _context.Entry(updatedProduct).Property(p => p.Description).IsModified = true;
            _context.Entry(updatedProduct).Property(p => p.MinimumQuantity).IsModified = true;

            await _context.SaveChangesAsync();
        }


    }
}
