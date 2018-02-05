using Domain.Entities;
using DynamicRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IUserRepository : IRepository<int, User>
    {
    }
}
