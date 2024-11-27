﻿using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        private readonly AppDbContext _context;
        public RepositoryUser(AppDbContext context) :base(context)
        {
            _context = context;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}