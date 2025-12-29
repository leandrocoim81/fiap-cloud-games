using FCG.SharedKernel;

namespace FCG.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public string Code { get; }
        public ValidationException(ErrorCode error)
            : base(error.Message)
        {
            Code = error.Code;
        }
    }
}
