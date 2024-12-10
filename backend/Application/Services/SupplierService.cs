using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
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
    public class SupplierService : ISupplierService
    {
        private readonly IRepositorySupplier _repositorySupplier;

        public SupplierService (IRepositorySupplier repositorySupplier)
        {
            _repositorySupplier = repositorySupplier;
        }

        public async Task<List<SupplierDTO>> GetAllAsync()
        {
            try
            {
                var listObj = await _repositorySupplier.GetAllAsync()
                    ?? throw new NotFoundException("Supplier not found");
                return SupplierDTO.CreateList(listObj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Supplier not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<SupplierDTO> GetByIdAsync(int id)
        {
            try
            {
                var obj = await FoundSupplierByIdAsync(id);
                return SupplierDTO.Create(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Suplier not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }

        public async Task<SupplierDTO> CreateAsync (SupplierCreateRequestDTO request)
        {
            try
            {
                await FoundSupplierByNameAsync(request.Name);

                var obj = new Supplier();
                obj.Name = request.Name;
                obj.Description = request.Description;
                obj.Tel = request.Tel;
                obj.Address = request.Address;

                var newObj = await _repositorySupplier.AddAsync(obj);
                return SupplierDTO.Create(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }

        public async Task UpdateAsync (SupplierUpdateRequestDTO request, int id)
        {
            try
            {
                var obj = await FoundSupplierByIdAsync(id);

                obj.Description = request.Description;
                obj.Tel = request.Tel;
                obj.Address = request.Address;
                obj.Name = request.Name;

                await _repositorySupplier.UpdateAsync(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Supplier not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }

        public async Task DeleteAsync (int id)
        {
            try
            {
                var obj = await FoundSupplierByIdAsync(id);
                await _repositorySupplier.DeleteAsync(obj);
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

        public async Task<SupplierDTO> GetByNameAsync(string name)
        {
            try
            {
                var obj = await _repositorySupplier.GetByNameAsync(name)
                    ?? throw new NotFoundException("Supplier not Found with name.");
                return SupplierDTO.Create(obj);
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException("Supplier not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }




        private async Task<Supplier> FoundSupplierByIdAsync(int id)
        {
            var obj = await _repositorySupplier.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Supplier by Id");
            return obj;
        }
        private async Task FoundSupplierByNameAsync(string name)
        {
            var exist = await _repositorySupplier.GetByNameAsync(name);

            if (exist != null) throw new Exception("Supplier name already exists");
        }
    }
}
