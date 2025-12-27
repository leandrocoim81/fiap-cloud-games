using FCG.Domain.Errors;
using FCG.Domain.Validation;
using System.Net.Mail;

namespace FCG.Domain.Policies.User
{
    public static class EmailPolicy
    {
        public static ValidationResult Validate(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return ValidationResult.Fail(DomainErrors.User.EmailIsNullOrWhiteSpace);

            try
            {
                var _ = new MailAddress(email.Trim());
                return ValidationResult.Ok();
            }
            catch
            {
                return ValidationResult.Fail(DomainErrors.User.InvalidEmail);
            }
        }
    }
}
