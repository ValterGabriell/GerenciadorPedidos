using GerenciadorDePedidos.Core.Domain.ValueObjects;
using GerenciadorDePedidos.Core.Library.Constantes;
using GerenciadorDePedidos.Core.Library.ValueObjects;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace GerenciadorDePedidos.Core.Testes.ValueObjects
{
    public class VOPrecoUnitarioTest
    {
        [Fact]
        public void Deve_Criar_VOPrecoUnitario_Valido()
        {
            var preco = 10.50m;
            var vo = new VOPrecoUnitario(preco);
            Assert.Equal(preco, vo._VOPrecoUnitario);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Nao_Deve_Criar_VOPrecoUnitario_Menor_Ou_Igual_Zero(decimal preco)
        {
            var ex = Assert.Throws<ValidationException>(() => new VOPrecoUnitario(preco));
            Assert.Contains(ConstantesAuxiliares.ERR_PRECO_UNITARIO_MENOR_OU_IGUAL_ZERO, ex.Message);
        }

        [Fact]
        public void Nao_Deve_Criar_VOPrecoUnitario_Maior_Que_Um_Milhao()
        {
            var ex = Assert.Throws<ValidationException>(() => new VOPrecoUnitario(1_000_000.01m));
            Assert.Contains(ConstantesAuxiliares.ERR_PRECO_UNITARIO_MUITO_ALTO, ex.Message);
        }

        [Theory]
        [InlineData(10.123)]
        [InlineData(0.999)]
        public void Nao_Deve_Criar_VOPrecoUnitario_Com_Mais_De_Duas_Casas_Decimais(decimal preco)
        {
            var ex = Assert.Throws<ValidationException>(() => new VOPrecoUnitario(preco));
            Assert.Contains(ConstantesAuxiliares.ERR_PRECO_UNITARIO_MUITAS_CASAS_DECIMAIS, ex.Message);
        }
    }
}
