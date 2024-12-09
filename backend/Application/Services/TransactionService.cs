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
        private readonly IRepositoryProduct _repositoryProduct;
        private readonly IRepositoryBranchTransaction _repositoryBranchTransaction;
        private readonly IRepositoryBranch _repositoryBranch;
        
        public TransactionService(IRepositoryTransaction repositoryTransaction, IRepositoryProduct repositoryProduct, IRepositoryBranchTransaction repositoryBranchTransaction, IRepositoryBranch repositoryBranch)
        {
            _repositoryTransaction = repositoryTransaction;
            _repositoryProduct = repositoryProduct;
            _repositoryBranchTransaction = repositoryBranchTransaction;
            _repositoryBranch = repositoryBranch;
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
                var product = await FoundProductByIdAsync(request.ProductId);
                var branch = await FoundBranchByIdAsync(request.BranchId);

                var branchsTransaction = await _repositoryTransaction.GetTransacionByBranchAsync(branch.Id, product.Id); 
                //puede devolver una []
                var lastTransactionProduct = branchsTransaction.OrderByDescending(t => t?.Id).FirstOrDefault();

                var newTransaction = CalculateTransaction(lastTransactionProduct, request);

                var lastTransaction = await _repositoryTransaction.GetLastTransactionByBranchAsync(branch.Id);
                if(lastTransaction != null)
                {
                    newTransaction.Money = request.IsIncome
                    ? lastTransaction.Money + request.Money
                    : lastTransaction.Money - request.Money;
                }
                
                var newObj = await _repositoryTransaction.AddAsync(newTransaction);
                
                var objBt = new BranchTransaction();
                objBt.BranchId = branch.Id;
                objBt.TransactionId = newObj.Id;
                var newObjBt = await _repositoryBranchTransaction.AddAsync(objBt);

                return TransactionDTO.Create(newObj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("ProductId or BrnachId Not Found", ex);
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
                var product = await FoundProductByIdAsync(request.ProductId);

                var obj = await FoundTransactionByIdAsync(id);
                obj.ProductId= product.Id;

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

        private Transaction CalculateTransaction(Transaction? lastTransactionProduct, TransactionCreateRequestDTO request)
        {
            var obj = new Transaction();
            obj.IsIncome = request.IsIncome;
            obj.ProductId = request.ProductId;

            if (lastTransactionProduct != null)
            {
                obj.Money = request.IsIncome
                    ? lastTransactionProduct.Money + request.Money
                    : lastTransactionProduct.Money - request.Money;

                obj.Quantity = request.Quantity;

                obj.TotalQuantity = request.IsIncome
                    ? lastTransactionProduct.TotalQuantity - request.Quantity
                    : lastTransactionProduct.TotalQuantity + request.Quantity;
            }
            else
            {
                obj.Money = request.Money;
                obj.Quantity = request.Quantity;
                obj.TotalQuantity = request.Quantity;
            }

            return obj;
        }


        private async Task<Transaction> FoundTransactionByIdAsync(int id)
        {
            var transaction = await _repositoryTransaction.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Transaction Id");
            return transaction;
        }

        private async Task<Product> FoundProductByIdAsync(int id)
        {
            var product = await _repositoryProduct.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Product Id");
            return product;
        }

        private async Task<Branch> FoundBranchByIdAsync(int id)
        {
            var branch = await _repositoryBranch.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Branch Id");
            return branch;
        }

    }
}
