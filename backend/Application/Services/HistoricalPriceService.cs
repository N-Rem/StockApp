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
    public class HistoricalPriceService : IHistoricalPriceService
    {
        private readonly IRepositoryHistoricalPrice _repositoryHistoricalPrice;

        public HistoricalPriceService (IRepositoryHistoricalPrice repositoryHistoricalPrice)
        {
            _repositoryHistoricalPrice = repositoryHistoricalPrice;
        }

        public async Task<IEnumerable<HistoricalPriceDTO>> GetAllAsinc()
        {
            try
            {
                var list = await _repositoryHistoricalPrice.GetAllAsync()
                    ?? throw new NotFoundException("Historical Prices not found");
                return HistoricalPriceDTO.CreateList(list);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Historical Prices not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<HistoricalPriceDTO> GetByIdAsync(int id)
        {
            try
            {
               var obj = await FoundHistoricalPriceByIdAsync(id);
                return HistoricalPriceDTO.Create(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Historical Prices not found", ex);
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
                var obj = await FoundHistoricalPriceByIdAsync(id);
                await _repositoryHistoricalPrice.DeleteAsync(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Historical Prices not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<IEnumerable<HistoricalPriceDTO>> GetByProductAsync(int idProduct)
        {
            try
            {
                var list = await _repositoryHistoricalPrice.GetByProductAsync(idProduct)
                    ?? throw new Exception("Historical Price not found by productId");
                
                return HistoricalPriceDTO.CreateList(list);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Historical Prices not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }




        private async Task<HistoricalPrice> FoundHistoricalPriceByIdAsync(int id)
        {
            var hp = await _repositoryHistoricalPrice.GetByIdAsync(id)
                ?? throw new NotFoundException("Historical Prices not found by id");
            return hp;
        }

    }
}
