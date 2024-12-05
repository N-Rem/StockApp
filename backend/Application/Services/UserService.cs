using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryUser _repositoryUser;
        public UserService(IRepositoryUser repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            try
            {
                var listUser = await _repositoryUser.GetAllAsync()
                    ?? throw new NotFoundException("Users not Found");

                return UserDTO.CreateList(listUser);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("User not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _repositoryUser.GetByIdAsync(id)
                ?? throw new NotFoundException("User not Found");
            var userDTO = UserDTO.Create(user);
            return userDTO;
        }

        public async Task<UserDTO> CreateOwnerAsync(UserCreateRequestDTO request)
        {
            try
            {
                await MailExistsAsync(request.Email);

                var newOwner = new User();
                newOwner.OwnerId = null;
                newOwner.Name = request.Name;
                newOwner.Email = request.Email;
                newOwner.Phone = request.Phone;
                newOwner.Type = Domain.Enums.UserType.Owner;
                newOwner.Password = request.Password;

                var owner = await _repositoryUser.AddAsync(newOwner);
                return UserDTO.Create(owner);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("User not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<UserDTO> CreateEmployeeAsync(UserCreateRequestDTO request)
        {
            try
            {
                await MailExistsAsync(request.Email);
                //Comporbar si existe el id del dueño.
                var newEmployee = new User();
                newEmployee.OwnerId = request.OwnerId;
                newEmployee.Name = request.Name;
                newEmployee.Email = request.Email;
                newEmployee.Phone = request.Phone;
                newEmployee.Type = Domain.Enums.UserType.Employee;
                newEmployee.Password = request.Password;

                var owner = await _repositoryUser.AddAsync(newEmployee);
                return UserDTO.Create(owner);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("User not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<UserDTO> CreateSysAdminAsync(UserCreateRequestDTO request)
        {
            try
            {
                await MailExistsAsync(request.Email);

                var newSysAdmin = new User();
                newSysAdmin.OwnerId = null;
                newSysAdmin.Name = request.Name;
                newSysAdmin.Email = request.Email;
                newSysAdmin.Phone = request.Phone;
                newSysAdmin.Type = Domain.Enums.UserType.SysAdmin;
                newSysAdmin.Password = request.Password;

                var sysAdmin = await _repositoryUser.AddAsync(newSysAdmin);
                return UserDTO.Create(sysAdmin);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("User not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
            
        }

        public async Task UpdateAsync(UserUpdateRequestDTO request, int id)
        {
            try
            {
            var user = await FoundUserIdAsync(id);
            user.Name = request.Name;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.Password = request.Password;

            await _repositoryUser.UpdateAsync(user);
            }
            catch(NotFoundException ex)
            {
                throw new NotFoundException("User Not Found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task DeleteAsync (int id)
        {
            try
            {
                var user = await FoundUserIdAsync(id);
                await _repositoryUser.DeleteAsync(user);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("User Not Found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }




        private async Task<User> FoundUserIdAsync(int id)
        {
            var user = await _repositoryUser.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found User Id");
            return user;
        }
        private async Task MailExistsAsync(String email)
        {
            var exist = await _repositoryUser.GetByEmailAsync(email);
            if (exist != null) throw new Exception("Email already exists");
        }
    }
}
