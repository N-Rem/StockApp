using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositoryBranch: RepositoryBase<Branch>, IRepositoryBranch
    {
        private readonly AppDbContext _context;
        public RepositoryBranch(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Branch> GetByNameAsync(string branchName)
        {
            var obj = await _context.Branches.FirstOrDefaultAsync(b => b.Name == branchName);
            return obj;
        }
    }
}
