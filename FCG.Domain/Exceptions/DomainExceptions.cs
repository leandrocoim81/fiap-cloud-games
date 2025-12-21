using FCG.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public string Code { get; }

        public DomainException(DomainError error)
            : base(error.Message)
        {
            Code = error.Code;
        }
    }
}
