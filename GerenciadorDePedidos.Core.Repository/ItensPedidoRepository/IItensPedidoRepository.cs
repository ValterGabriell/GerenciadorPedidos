using GerenciadorDePedidos.Core.Domain.Models;
using GerenciadorDePedidos.Core.Library.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Repository.ItensPedidoRepository
{
    public interface IItensPedidoRepository : IBaseInterface<ItemPedido>
    {
        void AdicionarVariosItensPedido(IEnumerable<ItemPedido> itensPedido);
        
    }
}
