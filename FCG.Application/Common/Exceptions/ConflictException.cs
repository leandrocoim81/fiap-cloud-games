using FCG.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Application.Common.Exceptions
{
    public class ConflictException:Exception
    {
        public string Code { get; }
        public ConflictException(ErrorCode error)
            : base(error.Message)
        {
            Code = error.Code;
        }
    }
}
