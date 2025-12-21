using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Application.Abstractions.Persistence
{
    public interface IGameRepository
    {
        Task<bool> ExistsByTitle(string title);
        Task<Guid> Add(Game game);
    }
}
