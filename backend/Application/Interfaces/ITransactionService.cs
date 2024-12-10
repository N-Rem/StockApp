using Application.Models;
using Application.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionDTO?>> GetAllAsync();
        Task<TransactionDTO> GetByIdAsync(int id);
        Task<TransactionDTO> CreateAsync(TransactionCreateRequestDTO request);
        Task UpdateAsync(TransactionUpdateRequestDTO request, int id);
        Task DeleteAsync(int id);


    }
}
