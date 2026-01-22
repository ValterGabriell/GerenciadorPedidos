using GerenciadorDePedidos.Core.Domain.Models;
using GerenciadorDePedidos.Core.Library.Constantes;
using GerenciadorDePedidos.Core.Library.Enum;
using GerenciadorDePedidos.Core.Library.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Dto.PedidosDTO
{
    public record DtoPedidos();

    public record DtoCriarPedidos(string ClienteNome,ICollection<DtoItensPedidosV2> ItensPedidos);
    public record DtoItensPedidosV2(string ProdutoNome, int Quantidade, decimal PrecoUnitario);
    public record DtoItensPedidos(string ProdutoNome, int Quantidade, decimal PrecoUnitario, Guid PedidoID);
    public record DtoItensPedidosPorId
    {
        public Guid Id { get; init; }
        public Guid PedidoID { get; init; }
        public string ProdutoNome { get; init; } = string.Empty;
        public int Quantidade { get; init; }
        public decimal PrecoUnitario { get; init; }

        public DtoItensPedidosPorId(ItemPedido ItemPedido)
        {
            Id = ItemPedido.Id;
            PedidoID = ItemPedido.PedidoId;
            ProdutoNome = ItemPedido.ProdutoNome._VOProdutoNome;
            Quantidade = ItemPedido.Quantidade;
            PrecoUnitario = ItemPedido.PrecoUnitario._VOPrecoUnitario;
        }
    }
    public record DtoAtualizarPedidos(Guid Id, string ClienteNome);
    public record DtoListarPedidos(Guid Id, string ClienteNome, DateTime DataCriacao, StatusPedido StatusPedido, decimal ValorTotal);
    public record DtoPedidoPorId(Guid Id, string ClienteNome, DateTime DataCriacao, StatusPedido StatusPedido, decimal ValorTotal, ICollection<DtoItensPedidosPorId> ItensDoPedido);
}
