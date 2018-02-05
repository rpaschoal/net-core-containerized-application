using Domain.Contracts;
using Domain.Entities;
using DynamicRepository.EFCore;
using Infrastructure.Data.EFCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<int, User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
