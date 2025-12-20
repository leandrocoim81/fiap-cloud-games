using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Application.Abstractions.Security
{
    public interface IPasswordHasher
    {
        string Hash(string password);
    }
}
