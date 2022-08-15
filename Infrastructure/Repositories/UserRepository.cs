﻿using Core.IServices.IRepositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : EntityRepository<User>,IUserRepository
    {
        private readonly ApplicationContext _applicationContext;
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<User> GetUser(int id)
        {
           return await _applicationContext.Users.FirstOrDefaultAsync(user=>user.Id == id);
        }
        public async Task<User> IsUserExists(string email , string passwordHash)
        {
            return await _applicationContext.Users.FirstOrDefaultAsync(user => user.Email == email && user.PasswordHash == passwordHash);
        }
    }
}
