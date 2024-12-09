using Application.Models;
using Application.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);

        Task<ProductDTO> CreateAsync(ProductCreateRequestDTO request);
        Task UpdateAsync(ProductUpdateRequestDTO request, int id);
        Task UpdateExceptPriceAsync(ProductUpdateRequestDTO request, int id);
        Task UpdateProductPriceAsync(ProductUpdateRequestDTO request, int id);

        Task DeleteAsync(int id);
        Task<ProductDTO> GetByNameAsync(string name);



    }
}
