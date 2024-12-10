using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IHistoricalPriceService
    {
        Task<IEnumerable<HistoricalPriceDTO>> GetAllAsinc();
        Task<HistoricalPriceDTO> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<HistoricalPriceDTO>> GetByProductAsync(int idProduct);

    }
}
