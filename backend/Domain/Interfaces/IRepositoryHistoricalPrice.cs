using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryHistoricalPrice : IRepositoryBase<HistoricalPrice>
    {
        Task<IEnumerable<HistoricalPrice>> GetByProductAsync(int idProduct);
    }
}
