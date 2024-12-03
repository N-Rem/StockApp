using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryProduct _repositoryProduct;
        public ProductService (IRepositoryProduct repositoryProduct)
        {
            _repositoryProduct = repositoryProduct;
        }

        public async Task<List<ProductDTO>> GetAllAsync ()
        {
            try
            {

                var listObj = await _repositoryProduct.GetAllAsync()
                    ?? throw new NotFoundException("Products not found");
                return ProductDTO.CreateList(listObj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Product not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            try
            {
                var obj = await _repositoryProduct.GetByIdAsync(id)
                    ?? throw new NotFoundException("Prodcut not found by Id");
                return ProductDTO.Create(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Product not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }

        public async Task<ProductDTO> CreateAsync(ProductCreateRequestDTO request)
        {
            try
            {
                await FoundProductByNameAsync(request.Name);

                var obj = new Product();
                obj.Name = request.Name;
                obj.Description = request.Description;
                obj.Price = request.Price;
                obj.MinimumQuantity = request.MinimumQuantity;

                var newObj = await _repositoryProduct.AddAsync(obj);
                return ProductDTO.Create(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }

        public async Task UpdateAsync(ProductUpdateRequestDTO request, int id)
        {
            try
            {
                var obj = await FoundProductByIdAsync(id);
                await FoundProductByNameAsync(request.Name);

                obj.Name = request.Name;
                obj.Description = request.Description;
                obj.Price = request.Price;
                obj.MinimumQuantity = request.MinimumQuantity;

                await _repositoryProduct.UpdateAsync(obj);

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Product not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var obj = await FoundProductByIdAsync(id);
                await _repositoryProduct.DeleteAsync(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Product not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }




        private async Task<Product> FoundProductByIdAsync(int id)
        {
            var product = await _repositoryProduct.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Product Id");
            return product;
        }

        private async Task FoundProductByNameAsync(string nameProduct)
        {
            var exist = await _repositoryProduct.GetByNameAsync(nameProduct);

            if (exist != null) throw new Exception("Product name already exists");
        }

    }
}
