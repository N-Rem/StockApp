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
        private readonly IRepositorySupplier _repositorySupplier;
        private readonly IRepositorySupplierProduct _repositorySupplierProduct;
        public ProductService (IRepositoryProduct repositoryProduct, IRepositorySupplier repositorySupplier, IRepositorySupplierProduct repositorySupplierProduct)
        {
            _repositoryProduct = repositoryProduct;
            _repositorySupplier = repositorySupplier;
            _repositorySupplierProduct = repositorySupplierProduct;
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
                var supplier = await FoundSupplierByIdAsync(request.SupplierId);

                var obj = new Product();
                obj.Name = request.Name;
                obj.Description = request.Description;
                obj.Price = request.Price;
                obj.MinimumQuantity = request.MinimumQuantity;

                var newObj = await _repositoryProduct.AddAsync(obj);

                var newObjSp = new SupplierProduct();
                newObjSp.ProductId = obj.Id;
                newObjSp.SupplierId = supplier.Id;
                await _repositorySupplierProduct.AddAsync(newObjSp);

                return ProductDTO.Create(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }

        public async Task UpdateAsync(ProductUpdateRequestDTO request, int id) //modifica todo
        {
            try
            {
                var obj = await FoundProductByIdAsync(id);

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

        public async Task UpdateExceptPriceAsync(ProductUpdateRequestDTO request, int id)
        {
            try
            {
                var obj = await FoundProductByIdAsync(id);

                obj.Name = request.Name;
                obj.Description = request.Description;
                obj.MinimumQuantity = request.MinimumQuantity;

                await _repositoryProduct.UpdateProductExceptPriceAsync(obj);
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

        public async Task UpdateProductPriceAsync(ProductUpdateRequestDTO request, int id)
        {
            try
            {
                var obj = await FoundProductByIdAsync(id);

                await _repositoryProduct.UpdateProductPriceAsync(obj, request.Price);
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

        public async Task<ProductDTO> GetByNameAsync(string name)
        {

            try
            {
                var obj = await _repositoryProduct.GetByNameAsync(name)
                    ?? throw new NotFoundException("Prodcut not found by Name");
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

        private async Task<Supplier> FoundSupplierByIdAsync(int id)
        {
            var obj = await _repositorySupplier.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Supplier by Id");
            return obj;
        }
    }
}
