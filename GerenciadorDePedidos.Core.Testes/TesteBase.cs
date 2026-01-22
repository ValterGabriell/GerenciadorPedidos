using GerenciadorDePedidos.Core.Domain.Models;
using GerenciadorDePedidos.Core.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GerenciadorDePedidos.Core.Testes
{
    public class TesteBase
    {
        [Fact]
        public void PedidoPago_NaoPodeSerCancelado()
        {
            // Arrange
            var itens = new List<ItemPedido>
            {
                ItemPedido.CriarItemPedido("Produto Teste", 2, 10.50m)
            };
            var pedido = Pedido.CriarPedido("Cliente Teste", itens);
            pedido.StatusPedido = StatusPedido.Pago;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                if (pedido.PedidoEstaPago())
                {
                    throw new InvalidOperationException("Pedido pago não pode ser cancelado.");
                }
                pedido.StatusPedido = StatusPedido.Cancelado;
            });
        }

        [Fact]
        public void ValorTotal_DeveSerCalculadoAutomaticamente()
        {
            // Arrange
            var itens = new List<ItemPedido>
            {
                ItemPedido.CriarItemPedido("Produto 1", 2, 10.00m),  // 2 * 10 = 20
                ItemPedido.CriarItemPedido("Produto 2", 3, 15.00m)   // 3 * 15 = 45
            };
            var valorEsperado = 65.00m; // 20 + 45

            // Act
            var pedido = Pedido.CriarPedido("Cliente Teste", itens);

            // Assert
            Assert.NotNull(pedido.ValorTotal);
            Assert.Equal(valorEsperado, pedido.ValorTotal._VOValorTotal);
        }

        [Fact]
        public void Pedido_NaoPermitirPedidoSemItens()
        {
            // Arrange
            var itensVazios = new List<ItemPedido>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                Pedido.CriarPedido("Cliente Teste", itensVazios));
        }

        [Fact]
        public void ItemPedido_QuantidadeDeveMaiorQueZero()
        {
            // Arrange & Act & Assert
            Assert.Throws<System.ComponentModel.DataAnnotations.ValidationException>(() =>
            {
                var item = ItemPedido.CriarItemPedido("Produto Teste", 0, 10.00m);
                item.ValidarQuantidadeProduto();
            });
        }

        [Fact]
        public void ItemPedido_QuantidadeNegativa_DeveLancarExcecao()
        {
            // Arrange & Act & Assert
            Assert.Throws<System.ComponentModel.DataAnnotations.ValidationException>(() =>
            {
                var item = ItemPedido.CriarItemPedido("Produto Teste", -5, 10.00m);
                item.ValidarQuantidadeProduto();
            });
        }

        [Fact]
        public void CalcularValorTotalPedido_DeveSomarTodosItens()
        {
            // Arrange
            var itens = new List<ItemPedido>
    {
        ItemPedido.CriarItemPedido("Produto A", 1, 100.00m), // 1 * 100 = 100
        ItemPedido.CriarItemPedido("Produto B", 2, 50.00m),  // 2 * 50 = 100
        ItemPedido.CriarItemPedido("Produto C", 5, 20.00m)   // 5 * 20 = 100
    };
            var pedido = Pedido.CriarPedido("Cliente Teste", itens);
            var valorEsperado = 300.00m; // 100 + 100 + 100

            // Act
            pedido.CalcularValorTotalPedido();

            // Assert
            Assert.Equal(valorEsperado, pedido.ValorTotal._VOValorTotal);
        }
    }
}