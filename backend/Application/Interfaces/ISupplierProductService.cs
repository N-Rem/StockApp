using Application.Models;
using Application.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISupplierProductService
    {
        Task<List<SupplierProductDTO>> GetAllAsync();
        Task<SupplierProductDTO> GetByIdAsync(int id);
        Task<SupplierProductDTO> CreateAsync(SupplierProductCreateRequestDTO request);
        Task UpdateAsync(SupplierProductUpdateRequestDTO request, int id);
        Task DeleteAsync(int id);

    }
}
