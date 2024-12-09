using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryUser : IRepositoryBase<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetOwnerById(int id);
    }
}
