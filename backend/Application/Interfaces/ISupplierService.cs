using Application.Models;
using Application.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISupplierService
    {
        Task<List<SupplierDTO>> GetAllAsync();
        Task<SupplierDTO> GetByIdAsync(int id);
        Task<SupplierDTO> CreateAsync(SupplierCreateRequestDTO request);
        Task UpdateAsync(SupplierUpdateRequestDTO request, int id);
        Task DeleteAsync(int id);
        Task<SupplierDTO> GetByNameAsync(string name);
    }
}
