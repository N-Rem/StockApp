using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(int id);

        Task<UserDTO> CreateOwnerAsync(UserCreateRequestDTO request);
        Task<UserDTO> CreateEmployeeAsync(UserCreateRequestDTO request);
        Task<UserDTO> CreateSysAdminAsync(UserCreateRequestDTO request);

        Task UpdateAsync(UserUpdateRequestDTO request, int id);
        Task DeleteAsync(int id);


    }
}
