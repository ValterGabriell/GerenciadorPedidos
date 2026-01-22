using GerenciadorDePedidos.Core.Dto.ItensPedidoDTO;
using GerenciadorDePedidos.Core.Library.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Application.ItensPedidos
{
    public interface IItensPedidosService
    {
        Task<Guid> AdicionarItemPedido(DtoCriarItemPedido dtoCriarItemPedido);
        Task AdicionarVariosItensPedido(IEnumerable<DtoCriarItemPedido> dtosCriarItensPedido);
        Task<DtoListarItensPedido?> ObterItemPedidoPorIdAsync(Guid id);
        Task<(IEnumerable<DtoListarItensPedido> ItensPedido, int Count)> ListarItensPedidoAsync(IEnumerable<FiltrosDinamicos> filtros, int pageNumber, int pageSize);
        Task AtualizarItemPedido(Guid PedidoID, DtoAtualizarItemPedido dtoAtualizarItemPedido);
        Task RemoverItemPedido(Guid id);
    }
}
