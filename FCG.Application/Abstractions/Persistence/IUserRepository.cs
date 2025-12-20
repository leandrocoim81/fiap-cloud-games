using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Application.Abstractions.Persistence
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmail(string email);
        Task<Guid> Add(Domain.Entities.User user);
    }
}
