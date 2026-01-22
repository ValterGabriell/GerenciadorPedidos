using GerenciadorDePedidos.Core.Domain;
using GerenciadorDePedidos.Core.Domain.Models;
using GerenciadorDePedidos.Core.Library.Constantes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace GerenciadorDePedidos.Core.Repository.PedidosRepository
{
    public sealed class PedidosRepositoryImpl : BaseRepository<Pedido>, IPedidosRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PedidosRepositoryImpl> _logger;

        public PedidosRepositoryImpl(AppDbContext context, ILogger<PedidosRepositoryImpl> logger) : base(context, logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public override async Task<Pedido> RecuperarPorId(Guid id)
        {
            var entity = await _context.Pedidos
                    .Where(e => e.Id == id)
                    .Include(e => e.ItensDoPedido)
                  .FirstOrDefaultAsync();

            return entity ?? throw new KeyNotFoundException(
                $"Entidade pedido com ID: {id} não encontrada.");
        }

        public async Task AdicionarVariosProdutosAoPedido(Guid PedidoID, IEnumerable<ItemPedido> itensDoPedido)
        {
            _logger.LogInformation("[REPOSITORY] Adicionando vários produtos ao pedido. PedidoID: {PedidoID}, QuantidadeProdutos: {Quantidade}",
                PedidoID, itensDoPedido.Count());

            try
            {
                _logger.LogInformation("[REPOSITORY] Recuperando pedido do banco com itens. PedidoID: {PedidoID}", PedidoID);

                var pedido = await _context.Pedidos
                    .Where(e => e.Id == PedidoID)
                    .Include(p => p.ItensDoPedido)
                    .FirstOrDefaultAsync(p => p.Id == PedidoID);

                if (pedido == null)
                {
                    throw new KeyNotFoundException(ConstantesAuxiliares.ERR_PEDIDO_NAO_ENCONTRADO);
                }

                _logger.LogInformation("[REPOSITORY] Pedido recuperado. Adicionando novos itens. PedidoID: {PedidoID}", PedidoID);

                
                foreach (var item in itensDoPedido)
                {
                    _context.ItensPedido.Add(item);
                    pedido.ItensDoPedido.Add(item);
                }
                pedido.CalcularValorTotalPedido();

                _logger.LogInformation("[REPOSITORY] Produtos adicionados ao pedido com sucesso. PedidoID: {PedidoID}", PedidoID);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "[REPOSITORY] Pedido não encontrado. PedidoID: {PedidoID}", PedidoID);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[REPOSITORY] Erro ao adicionar produtos ao pedido. PedidoID: {PedidoID}, Mensagem: {Mensagem}",
                    PedidoID, ex.Message);
                throw;
            }
        }
    }
}
