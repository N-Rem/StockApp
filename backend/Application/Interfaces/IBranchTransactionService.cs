using Application.Models;
using Application.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBranchTransactionService
    {
        Task<IEnumerable<BranchTransactionDTO>> GetAllAsync();
        Task<BranchTransactionDTO> GetByIdAsync(int id);
        Task UpdateAsync(BranchTransactionUpdateRequestDTO request, int id);
        Task DeleteAsync(int id);

    }
}
