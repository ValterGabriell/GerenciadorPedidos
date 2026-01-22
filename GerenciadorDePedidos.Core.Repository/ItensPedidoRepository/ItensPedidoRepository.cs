using GerenciadorDePedidos.Core.Domain;
using GerenciadorDePedidos.Core.Domain.Models;
using GerenciadorDePedidos.Core.Repository.PedidosRepository;
using Microsoft.Extensions.Logging;

namespace GerenciadorDePedidos.Core.Repository.ItensPedidoRepository
{
    public sealed class ItensPedidoRepositoryImpl : BaseRepository<ItemPedido>,IItensPedidoRepository
    {
        private readonly AppDbContext _context;

        public ItensPedidoRepositoryImpl(AppDbContext context, ILogger<PedidosRepositoryImpl> logger) : base(context, logger)
        {
            this._context = context;
        }

        public void AdicionarVariosItensPedido(IEnumerable<ItemPedido> itensPedido) => this._context.ItensPedido.AddRange(itensPedido);

      
    }
}
