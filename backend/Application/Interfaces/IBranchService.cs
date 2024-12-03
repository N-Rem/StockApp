using Application.Models;
using Application.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDTO>> GetAllAsync();
        Task<BranchDTO> GetByIdAsync(int id);
        Task<BranchDTO> CreateAsync(BranchCreateRequestDTO request);
        Task UpdateAsync(BranchUpdateRequestDTO request, int id);
        Task DeleteAsync(int id);
        Task<BranchDTO> GetByName(string name);

    }
}
