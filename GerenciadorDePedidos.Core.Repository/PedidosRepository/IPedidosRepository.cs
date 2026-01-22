using GerenciadorDePedidos.Core.Domain.Models;
using GerenciadorDePedidos.Core.Library.Filtros;

namespace GerenciadorDePedidos.Core.Repository.PedidosRepository
{
    public interface IPedidosRepository : IBaseInterface<Pedido> 
    {
        Task AdicionarVariosProdutosAoPedido(Guid PedidoID, IEnumerable<ItemPedido> itensDoPedido);
    }
}