using Application.Interfaces;
using Application.Models;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryUser _repositoryUser ;
        public UserService(IRepositoryUser repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public async Task<List<UserDTO>>  GetAllAsync()
        {
            var listUser = await _repositoryUser.GetAllAsync()
                ?? throw new NotFoundException("Users not Found");

            return UserDTO.CreateList(listUser);
        }
    }
}
