using FCG.Domain.Policies.User;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FCG.UnitTests.Domain.Policies
{
    public class PasswordPolicyTests
    {
        [Fact]
        public void Deve_retornar_erro_quando_senha_for_nula()
        {
            var result = PasswordPolicy.Validate(null);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Deve_retornar_erro_quanto_senha_for_vazia()
        {
            var result = PasswordPolicy.Validate("");
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Deve_retornar_erro_quando_senha_for_menor_que_o_tamanho_minimo()
        {
            var result = PasswordPolicy.Validate("Ab1!3gd");
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Deve_retornar_erro_quando_senha_nao_contiver_letra()
        {
            var result = PasswordPolicy.Validate("12345678!");
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Deve_retornar_erro_quando_senha_nao_contiver_numero()
        {
            var result = PasswordPolicy.Validate("Abcdefgh!");
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Deve_retornar_erro_quando_senha_nao_contiver_caractere_especial()
        {
            var result = PasswordPolicy.Validate("Abcdefg1");
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Deve_retornar_erro_quando_senha_for_apenas_espacos_em_branco()
        {
            var result = PasswordPolicy.Validate("       ");
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Deve_retornar_sucesso_quando_senha_for_valida()
        {
            var result = PasswordPolicy.Validate("Abcdef1!");
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Deve_retornar_sucesso_quando_senha_for_valida_com_espacos_em_branco()
        {
            var result = PasswordPolicy.Validate("  Abcdef1!  ");
            Assert.True(result.IsValid);
        }        
    }
}
