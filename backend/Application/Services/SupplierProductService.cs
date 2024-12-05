using Application.Interfaces;
using Application.Models.Requests;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SupplierProductService : ISupplierProductService
    {
        private readonly IRepositorySupplierProduct _repositorySupplierProduct;
        private readonly IRepositorySupplier _repositorySupplier;
        private readonly IRepositoryProduct _repositoryProduct;

        public SupplierProductService(IRepositorySupplierProduct repositorySupplierProduct, IRepositorySupplier repositorySupplier, IRepositoryProduct repositoryProduct)
        {
            _repositorySupplierProduct = repositorySupplierProduct;
            _repositorySupplier = repositorySupplier;
            _repositoryProduct = repositoryProduct;
        }


        public async Task<List<SupplierProductDTO>> GetAllAsync()
        {
            try
            {
                var listObj = await _repositorySupplierProduct.GetAllAsync()
                    ?? throw new NotFoundException("SupplierProducts not found");
                return SupplierProductDTO.CreateList(listObj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("SupplierProduct not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<SupplierProductDTO> GetByIdAsync(int id)
        {
            try
            {
                var obj = await _repositorySupplierProduct.GetByIdAsync(id)
                    ?? throw new NotFoundException("SupplierProdcut not found by Id");
                return SupplierProductDTO.Create(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("SupplierProduct not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }

        public async Task<SupplierProductDTO> CreateAsync(SupplierProductCreateRequestDTO request)
        {
            try
            {
                await FoundProductByIdAsync(request.ProductId);
                await FoundSupplierByIdAsync(request.SupplierId);

                var obj = new SupplierProduct();
                obj.ProductId = request.ProductId;
                obj.SupplierId = request.SupplierId;
                var newObj = await _repositorySupplierProduct.AddAsync(obj);
                return SupplierProductDTO.Create(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }

        public async Task UpdateAsync(SupplierProductUpdateRequestDTO request, int id)
        {
            try
            {
                var obj = await FoundSupplierProductByIdAsync(id);
                await FoundSupplierByIdAsync(request.SupplierId);
                await FoundProductByIdAsync(request.ProductId);

                obj.ProductId = request.ProductId;
                obj.SupplierId = request.SupplierId;

                await _repositorySupplierProduct.UpdateAsync(obj);

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("SupplierProduct not found", ex);
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
                var obj = await FoundSupplierProductByIdAsync(id);
                await _repositorySupplierProduct.DeleteAsync(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("SupplierProduct not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }



        private async Task<SupplierProduct> FoundSupplierProductByIdAsync(int id)
        {
            var supplierProduct = await _repositorySupplierProduct.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found SupplierProduct Id");
            return supplierProduct;
        }

        private async Task FoundProductByIdAsync(int id)
        {
            var product = await _repositoryProduct.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Product Id");
        }

        private async Task FoundSupplierByIdAsync(int id)
        {
            var obj = await _repositorySupplier.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Supplier by Id");
        }
    }
}
