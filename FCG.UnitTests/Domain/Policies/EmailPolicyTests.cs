using FCG.Domain.Policies;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FCG.UnitTests.Domain.Policies
{
    public class EmailPolicyTests
    {
        [Fact]
        public void Deve_retornar_erro_quando_email_for_nulo()
        {
            var result = EmailPolicy.Validate(null);
            Assert.False(result.IsValid);
        }
        [Fact]
        public void Deve_retornar_erro_quando_email_for_vazio()
        {
            var result = EmailPolicy.Validate("");
            Assert.False(result.IsValid);
        }
        [Fact]
        public void Deve_retornar_erro_quando_email_for_invalido()
        {
            var result = EmailPolicy.Validate("email_invalido");
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Deve_retornar_sucesso_quando_email_for_valido()
        {
            var result = EmailPolicy.Validate("teste@teste.com");
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Deve_retornar_sucesso_quando_email_for_valido_com_espacos_em_branco()
        {
            var result = EmailPolicy.Validate("  teste@teste.com  ");
            Assert.True(result.IsValid);
        }
    }
}