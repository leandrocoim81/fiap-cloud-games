using FCG.Domain.Policies.Game;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FCG.UnitTests.Domain.Policies
{
    public class GamePolicyTests
    {
        [Fact]
        public void Deve_retornar_erro_quando_title_for_nulo()
        {
            var result = GamePolicy.Validate(null, "Description", 10);
            Assert.False(result.IsValid);
        }
        [Fact]
        public void Deve_retornar_erro_quando_title_for_vazio()
        {
            var result = GamePolicy.Validate("", "Description", 10);
            Assert.False(result.IsValid);
        }

    }
}
