using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Application.UseCases.Auth.RegisterUser
{
    public record RegisterUserCommand(string Name, string Email, string Password);
}
