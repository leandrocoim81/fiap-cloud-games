using FCG.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace FCG.Domain.Policies
{
    public static class EmailPolicy
    {
        public static EmailValidationResult Validate(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return EmailValidationResult.Fail(DomainErrors.User.EmailIsNullOrWhiteSpace);

            try
            {
                var _ = new MailAddress(email.Trim());
                return EmailValidationResult.Ok();
            }
            catch
            {
                return EmailValidationResult.Fail(DomainErrors.User.InvalidEmail);
            }
        }

        public readonly record struct EmailValidationResult(bool IsValid, DomainError? Error)
        {
            public static EmailValidationResult Ok() => new(true, null);
            public static EmailValidationResult Fail(DomainError error) => new(false, error);
        }
    }
}
