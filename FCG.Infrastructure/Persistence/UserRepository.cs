using FCG.Application.Abstractions.Persistence;
using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        public Task<Guid> Add(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
