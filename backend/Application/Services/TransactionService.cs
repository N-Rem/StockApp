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
using Application.Interfaces;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepositoryTransaction _repositoryTransaction;
        
        public TransactionService(IRepositoryTransaction repositoryTransaction)
        {
            _repositoryTransaction = repositoryTransaction;
        }


        public async Task<List<TransactionDTO?>> GetAllAsync()
        {
            try
            {
                var listObj = await _repositoryTransaction.GetAllAsync()
                    ?? throw new NotFoundException("Transaction not found");
                return TransactionDTO.CreateList(listObj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Transaction not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<TransactionDTO> GetByIdAsync(int id)
        {
            try
            {
                var obj = await FoundTransactionByIdAsync(id)
                    ?? throw new NotFoundException("Transaction not found by Id");
                return TransactionDTO.Create(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Transaction not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }

        public async Task<TransactionDTO> CreateAsync(TransactionCreateRequestDTO request)
        {
            try
            {
                var obj = new Transaction();
                obj.Money = request.Money;
                obj.TotalQuantity = request.TotalQuantity;
                obj.Quantity = request.Quantity;
                obj.IsIncome = request.IsIncome;

                obj.ProductId = request.ProductId;
                
                var newObj = await _repositoryTransaction.AddAsync(obj);
                return TransactionDTO.Create(newObj);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }

        public async Task UpdateAsync(TransactionUpdateRequestDTO request, int id)
        {
            try
            {
                var obj = await FoundTransactionByIdAsync(id);

                obj.Money = request.Money;
                obj.TotalQuantity = request.TotalQuantity;
                obj.Quantity = request.Quantity;
                obj.IsIncome = request.IsIncome;
                obj.ProductId= request.ProductId;

                await _repositoryTransaction.UpdateAsync(obj);

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Transaction not found", ex);
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
                var obj = await FoundTransactionByIdAsync(id);
                await _repositoryTransaction.DeleteAsync(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Transaction not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An Unexpected error ocurred", ex);
            }
        }



        private async Task<Transaction> FoundTransactionByIdAsync(int id)
        {
            var transaction = await _repositoryTransaction.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Transaction Id");
            return transaction;
        }

    }
}
