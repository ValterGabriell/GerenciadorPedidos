using GerenciadorDePedidos.Core.Library.Constantes;
using GerenciadorDePedidos.Core.Library.ValueObjects;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace GerenciadorDePedidos.Core.Testes.ValueObjects
{
    public class VOValorTotalPedidoTest
    {
        [Fact]
        public void Deve_Criar_VOValorTotalPedido_Valido()
        {
            var valor = 100.50m;
            var vo = new VOValorTotalPedido(valor);
            Assert.Equal(valor, vo._VOValorTotal);
        }

        [Theory]
        [InlineData(-0.01)]
        [InlineData(-100)]
        public void Nao_Deve_Criar_VOValorTotalPedido_Negativo(decimal valor)
        {
            var ex = Assert.Throws<ValidationException>(() => new VOValorTotalPedido(valor));
            Assert.Contains(ConstantesAuxiliares.ERR_VALOR_TOTAL_PEDIDO_NEGATIVO, ex.Message);
        }

        [Fact]
        public void Nao_Deve_Criar_VOValorTotalPedido_Maior_Que_Dez_Milhoes()
        {
            var ex = Assert.Throws<ValidationException>(() => new VOValorTotalPedido(10_000_000.01m));
            Assert.Contains(ConstantesAuxiliares.ERR_VALOR_TOTAL_PEDIDO_MUITO_ALTO, ex.Message);
        }

        [Theory]
        [InlineData(10.123)]
        [InlineData(0.999)]
        public void Nao_Deve_Criar_VOValorTotalPedido_Com_Mais_De_Duas_Casas_Decimais(decimal valor)
        {
            var ex = Assert.Throws<ValidationException>(() => new VOValorTotalPedido(valor));
            Assert.Contains(ConstantesAuxiliares.ERR_VALOR_TOTAL_PEDIDO_MUITAS_CASAS_DECIMAIS, ex.Message);
        }
    }
}
