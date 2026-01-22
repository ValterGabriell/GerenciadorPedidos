using GerenciadorDePedidos.Core.Dto.PedidosDTO;
using GerenciadorDePedidos.Core.Library.Enum;
using GerenciadorDePedidos.Core.Library.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Application.Pedidos
{
    public interface IPedidosService
    {
        Task<Guid> AdicionarPedido(DtoCriarPedidos dtoCriarPedidos);
        Task<bool> AlterarStatusPedido(Guid PedidoID, StatusPedido NovoStatusPedido);
        Task AdicionarVariosProdutosAoPedido(Guid PedidoID, IEnumerable<DtoItensPedidosV2> dtoItensPedido);
        Task<DtoPedidoPorId?> ObterPedidoPorIdAsync(Guid id);
        Task<(IEnumerable<DtoListarPedidos> Pedidos, int Count)> ListarPedidosAsync(IEnumerable<FiltrosDinamicos> filtros, int pageNumber, int pageSize);
        Task AtualizarPedido(DtoAtualizarPedidos dtoAtualizarPedidos);
        Task RemoverPedido(Guid id);
    }
}
