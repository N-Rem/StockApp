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
    public class BranchTransactionService : IBranchTransactionService
    {
        private readonly IRepositoryBranchTransaction _repositoryBranchTransaction;
        private readonly IRepositoryBranch _repositoryBranch;
        private readonly IRepositoryTransaction _repositoryTransaction;

        public BranchTransactionService(IRepositoryBranchTransaction repositoryBranchTransaction, IRepositoryBranch repositoryBranch, IRepositoryTransaction repositoryTransaction)
        {
            _repositoryBranchTransaction = repositoryBranchTransaction;
            _repositoryBranch = repositoryBranch;
            _repositoryTransaction = repositoryTransaction;
        }

        public async Task<IEnumerable<BranchTransactionDTO>> GetAllAsync()
        {
            try
            {
                var listObj = await _repositoryBranchTransaction.GetAllAsync()
                    ?? throw new NotFoundException("BranchesTransaction Not Found");
                return BranchTransactionDTO.CreateList(listObj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("BranchTransaction not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<BranchTransactionDTO> GetByIdAsync(int id)
        {
            try
            {
                var obj = await FoundBranchTransactionByIdAsync(id);
                return BranchTransactionDTO.Create(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("BranchTransaction not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<BranchTransactionDTO> CreateAsync(BranchTransactionCreateRequestDTO request)
        {
            try
            {
                //Fucnion para verificar si existen los ids
                await FoundBranchByIdAsync(request.BranchId);
                await FoundTransactionByIdAsync(request.TransactionId);

                var obj = new BranchTransaction();

                obj.TransactionId = request.TransactionId;
                obj.BranchId = request.BranchId;

                var newObj = await _repositoryBranchTransaction.AddAsync(obj);
                return BranchTransactionDTO.Create(newObj);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }

        }

        public async Task UpdateAsync(BranchTransactionUpdateRequestDTO request, int id)
        {
            try
            {
                var obj = await FoundBranchTransactionByIdAsync(id);
                await FoundBranchByIdAsync(request.BranchId);
                await FoundTransactionByIdAsync(request.TransactionId);

                obj.BranchId = request.BranchId;
                obj.TransactionId = request.TransactionId;

                await _repositoryBranchTransaction.UpdateAsync(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("BranchTransaction not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var obj = await FoundBranchTransactionByIdAsync(id);
                await _repositoryBranchTransaction.DeleteAsync(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("BranchTransaction not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }


        private async Task<BranchTransaction> FoundBranchTransactionByIdAsync(int id)
        {
            var branchTransaction = await _repositoryBranchTransaction.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found BranchTransaction Id");
            return branchTransaction;
        }

        private async Task FoundTransactionByIdAsync(int id)
        {
            var transaction = await _repositoryTransaction.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Transaction Id");
        }

        private async Task FoundBranchByIdAsync(int id)
        {
            var branch = await _repositoryBranch.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Branch Id");
        }


    }
}
