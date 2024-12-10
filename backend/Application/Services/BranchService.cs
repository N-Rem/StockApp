using Application.Models;
using Domain.Interfaces;
using Domain.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Requests;
using Application.Interfaces;

namespace Application.Services
{
    public class BranchService: IBranchService
    {
        private readonly IRepositoryBranch _repositoryBranch;
        private readonly IRepositoryUser _repositoryUser;

        public BranchService(IRepositoryBranch repositoriesBranch, IRepositoryUser repositoryUser)
        {
            _repositoryBranch = repositoriesBranch;
            _repositoryUser = repositoryUser;
        }

        public async Task<IEnumerable<BranchDTO>> GetAllAsync()
        {
            try
            {
                var listObj = await _repositoryBranch.GetAllAsync()
                    ?? throw new NotFoundException("Branches Not Found");
                return BranchDTO.CreateList(listObj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Branch not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<BranchDTO> GetByIdAsync(int id)
        {
            try
            {
                var obj = await FoundBranchByIdAsync(id);
                return BranchDTO.Create(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Branch not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<BranchDTO> CreateAsync(BranchCreateRequestDTO request)
        {
            try
            {
                await FoundBranchByNameAsync(request.Name);
                var owner = await _repositoryUser.GetOwnerById(request.OwnerId)
                    ?? throw new NotFoundException("OwnerId not found");
                var obj = new Branch();
                obj.Name = request.Name;
                obj.Addres = request.Addres;
                obj.Description = request.Description;
                obj.Tel = request.Tel;
                obj.OwnerId = owner.Id;
                var newObj = await _repositoryBranch.AddAsync(obj);
                return BranchDTO.Create(newObj);
            }
            catch(NotFoundException ex)
            {
                throw new NotFoundException("OwnerId not found");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }

        }

        public async Task UpdateAsync(BranchUpdateRequestDTO request, int id)
        {
            try
            {
                var obj = await FoundBranchByIdAsync(id);

                obj.Name = request.Name;
                obj.Addres = request.Addres;
                obj.Description = request.Description;
                obj.Tel = request.Tel;

                await _repositoryBranch.UpdateAsync(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Branch not found", ex);
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
                var obj = await FoundBranchByIdAsync(id);
                await _repositoryBranch.DeleteAsync(obj);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Branch not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }


        public async Task<BranchDTO> GetByName(string name)
        {
            try
            {
                var obj = await _repositoryBranch.GetByNameAsync(name)
                    ?? throw new NotFoundException("Branch not Found with name.");
                return BranchDTO.Create(obj);
            }

            catch (NotFoundException ex)
            {
                throw new NotFoundException("Branch not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }




        private async Task<Branch> FoundBranchByIdAsync(int id)
        {
            var branch = await _repositoryBranch.GetByIdAsync(id)
                ?? throw new NotFoundException("Not Found Branch Id");
            return branch;
        }

        private async Task FoundBranchByNameAsync(string nameBranch)
        {
            var exist = await _repositoryBranch.GetByNameAsync(nameBranch);

            if (exist != null) throw new Exception("Branch name already exists");
        }
    }
}
