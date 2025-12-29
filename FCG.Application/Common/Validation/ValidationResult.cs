using FCG.SharedKernel;

namespace FCG.Application.Common.Validation
{
    public readonly record struct ValidationResult(bool IsValid, ErrorCode? Error)
    {
        public static ValidationResult Ok() => new(true, null);
        public static ValidationResult Fail(ErrorCode error) => new(false, error);
    }
}
