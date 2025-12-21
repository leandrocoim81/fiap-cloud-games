using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Domain.Errors
{
    public sealed record DomainError(string Code, string Message);

}
