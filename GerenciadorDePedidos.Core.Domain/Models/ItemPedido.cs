using GerenciadorDePedidos.Core.Domain.Validacoes;
using GerenciadorDePedidos.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Domain.Models
{
    public sealed class ItemPedido
    {
        private ItemPedido() { }
        
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }

        public VOProdutoNome ProdutoNome { get; set; } = new VOProdutoNome();
        public int Quantidade { get; set; }
        public VOPrecoUnitario PrecoUnitario { get; set; } = new VOPrecoUnitario();

        public void ValidarQuantidadeProduto()
        {
            ItemPedidoValidacoes validacoes = new();
            var resultado = validacoes.Validate(this);
            if (!resultado.IsValid) throw new ValidationException($"Validação falhou para atualizar pedido: {resultado.Errors[0]}");
        }
       
        public static ItemPedido CriarItemPedido(string produtoNome, int quantidade, decimal precoUnitario, Guid PedidoID)
        {
            var pedido = new ItemPedido
            {
                Id = Guid.NewGuid(),
                ProdutoNome = new VOProdutoNome(produtoNome),
                Quantidade = quantidade,
                PrecoUnitario = new VOPrecoUnitario(precoUnitario),
                PedidoId = PedidoID

            };
            pedido.ValidarQuantidadeProduto();
            return pedido;
        }

        public static ItemPedido CriarItemPedido(string produtoNome, int quantidade, decimal precoUnitario)
        {
            return new ItemPedido
            {
                Id = Guid.NewGuid(),
                ProdutoNome = new VOProdutoNome(produtoNome),
                Quantidade = quantidade,
                PrecoUnitario = new VOPrecoUnitario(precoUnitario),


            };
        }


    }
}
