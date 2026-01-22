using GerenciadorDePedidos.Core.Application.UnityOfWork;
using GerenciadorDePedidos.Core.Domain.Models;
using GerenciadorDePedidos.Core.Domain.ValueObjects;
using GerenciadorDePedidos.Core.Dto.PedidosDTO;
using GerenciadorDePedidos.Core.Library.Constantes;
using GerenciadorDePedidos.Core.Library.Enum;
using GerenciadorDePedidos.Core.Library.Filtros;
using Microsoft.Extensions.Logging;

namespace GerenciadorDePedidos.Core.Application.Pedidos
{
    public sealed class PedidoServiceImpl : IPedidosService
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly ILogger<PedidoServiceImpl> _logger;

        public PedidoServiceImpl(IUnityOfWork unityOfWork, ILogger<PedidoServiceImpl> logger)
        {
            this._unityOfWork = unityOfWork;
            this._logger = logger;
        }

        public async Task<Guid> AdicionarPedido(DtoCriarPedidos dtoCriarPedidos)
        {
            _logger.LogInformation("[SERVICE] Iniciando adição de pedido. Cliente: {Cliente}, QuantidadeItens: {QuantidadeItens}",
                dtoCriarPedidos.ClienteNome, dtoCriarPedidos.ItensPedidos.Count());

            try
            {
                var pedidoParaCriar = Pedido.CriarPedido(dtoCriarPedidos.ClienteNome);
                var itensDoPedido = dtoCriarPedidos.ItensPedidos
                    .Select(item => ItemPedido.CriarItemPedido(item.ProdutoNome, item.Quantidade, item.PrecoUnitario, pedidoParaCriar.Id))
                    .ToList();
                pedidoParaCriar.ItensDoPedido = itensDoPedido;
                pedidoParaCriar.CalcularValorTotalPedido();
                this._unityOfWork.PedidosRepository.Adicionar(pedidoParaCriar);
                await this._unityOfWork.SaveChangesAsync();

                _logger.LogInformation("[SERVICE] Pedido adicionado com sucesso. PedidoID: {PedidoID}, Cliente: {Cliente}",
                    pedidoParaCriar.Id, dtoCriarPedidos.ClienteNome);
                return pedidoParaCriar.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[SERVICE] Erro ao adicionar pedido. Cliente: {Cliente}, Mensagem: {Mensagem}",
                    dtoCriarPedidos.ClienteNome, ex.Message);
                throw;
            }
        }

        public async Task AdicionarVariosProdutosAoPedido(Guid PedidoID, IEnumerable<DtoItensPedidosV2> dtoItensPedido)
        {
            _logger.LogInformation("[SERVICE] Iniciando adição de vários produtos ao pedido. PedidoID: {PedidoID}, QuantidadeProdutos: {Quantidade}",
                PedidoID, dtoItensPedido.Count());

            try
            {
     
                var itens = dtoItensPedido
                    .Select(item => ItemPedido.CriarItemPedido(item.ProdutoNome, item.Quantidade, item.PrecoUnitario, PedidoID))
                    .ToList();

                await this._unityOfWork.PedidosRepository.AdicionarVariosProdutosAoPedido(PedidoID, itens);

                await this._unityOfWork.SaveChangesAsync();

                _logger.LogInformation("[SERVICE] Produtos adicionados com sucesso ao pedido {PedidoID}", PedidoID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[SERVICE] Erro ao adicionar produtos ao pedido {PedidoID}. Mensagem: {Mensagem}",
                    PedidoID, ex.Message);
                throw;
            }
        }

        public async Task<DtoPedidoPorId?> ObterPedidoPorIdAsync(Guid id)
        {
            _logger.LogInformation("[SERVICE] Buscando pedido por ID. PedidoID: {PedidoID}", id);

            try
            {
                var pedido = await this._unityOfWork.PedidosRepository.RecuperarPorId(id);

                if (pedido == null)
                {
                    _logger.LogWarning("[SERVICE] Pedido não encontrado no banco de dados. PedidoID: {PedidoID}", id);
                    return null;
                }

                _logger.LogInformation("[SERVICE] Pedido encontrado. PedidoID: {PedidoID}, Cliente: {Cliente}, ValorTotal: {ValorTotal}",
                    pedido.Id, pedido.ClienteNome._VOClienteNome, pedido.ValorTotal._VOValorTotal);

                return new DtoPedidoPorId(
                    pedido.Id,
                    pedido.ClienteNome._VOClienteNome,
                    pedido.DataCriacao,
                    pedido.StatusPedido,
                    pedido.ValorTotal._VOValorTotal,
                    pedido.ItensDoPedido.Select(e => new DtoItensPedidosPorId(e)).ToList()
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[SERVICE] Erro ao buscar pedido {PedidoID}. Mensagem: {Mensagem}", id, ex.Message);
                throw;
            }
        }

        public async Task<(IEnumerable<DtoListarPedidos> Pedidos, int Count)> ListarPedidosAsync(IEnumerable<FiltrosDinamicos> filtros, int pageNumber, int pageSize)
        {
            _logger.LogInformation("[SERVICE] Listando pedidos. PageNumber: {PageNumber}, PageSize: {PageSize}, Filtros: {Filtros}",
                pageNumber, pageSize, filtros.Count());

            try
            {
                var resultado = await this._unityOfWork.PedidosRepository.RecuperaLista(filtros, pageNumber, pageSize);

                var pedidosDto = resultado.Data.Select(pedido => new DtoListarPedidos(
                    pedido.Id,
                    pedido.ClienteNome._VOClienteNome,
                    pedido.DataCriacao,
                    pedido.StatusPedido,
                    pedido.ValorTotal._VOValorTotal
                ));

                _logger.LogInformation("[SERVICE] Pedidos listados com sucesso. Total: {Total}, Retornados: {Retornados}",
                    resultado.TotalCount, pedidosDto.Count());

                return (pedidosDto, resultado.TotalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[SERVICE] Erro ao listar pedidos. PageNumber: {PageNumber}, PageSize: {PageSize}, Mensagem: {Mensagem}",
                    pageNumber, pageSize, ex.Message);
                throw;
            }
        }

        public async Task AtualizarPedido(DtoAtualizarPedidos dtoAtualizarPedidos)
        {
            _logger.LogInformation("[SERVICE] Iniciando atualização de pedido. PedidoID: {PedidoID}, NovoCliente: {Cliente}",
                dtoAtualizarPedidos.Id, dtoAtualizarPedidos.ClienteNome);

            try
            {
                var pedidoExistente = await this._unityOfWork.PedidosRepository.RecuperarPorId(dtoAtualizarPedidos.Id);
                
                if (pedidoExistente != null)
                {
                    pedidoExistente.ClienteNome = new VOClienteNome(dtoAtualizarPedidos.ClienteNome);
                    await this._unityOfWork.PedidosRepository.Atualizar(dtoAtualizarPedidos.Id,pedidoExistente);
                    await this._unityOfWork.SaveChangesAsync();
                    _logger.LogInformation("[SERVICE] Pedido atualizado com sucesso. PedidoID: {PedidoID}", dtoAtualizarPedidos.Id);
                }
                else
                {
                    _logger.LogWarning("[SERVICE] Pedido não encontrado para atualização. PedidoID: {PedidoID}", dtoAtualizarPedidos.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[SERVICE] Erro ao atualizar pedido {PedidoID}. Mensagem: {Mensagem}",
                    dtoAtualizarPedidos.Id, ex.Message);
                throw;
            }
        }

        public async Task RemoverPedido(Guid id)
        {
            _logger.LogInformation("[SERVICE] Iniciando remoção de pedido. PedidoID: {PedidoID}", id);

            try
            {
                await this._unityOfWork.PedidosRepository.Remover(id);
                await this._unityOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[SERVICE] Erro ao remover pedido {PedidoID}. Mensagem: {Mensagem}", id, ex.Message);
                throw;
            }
        }

        public async Task<bool> AlterarStatusPedido(Guid PedidoID, StatusPedido NovoStatusPedido)
        {
            _logger.LogInformation("[SERVICE] Iniciando alteração de status de pedido. PedidoID: {PedidoID}, NovoStatus: {NovoStatusPedido}", 
                PedidoID, NovoStatusPedido);
            
            try
            {
                var pedido = await this._unityOfWork.PedidosRepository.RecuperarPorId(PedidoID) 
                    ?? throw new KeyNotFoundException($"Pedido com ID {PedidoID} não encontrado.");

                if (pedido.PedidoEstaPago() && NovoStatusPedido == StatusPedido.Cancelado) throw new InvalidOperationException(ConstantesAuxiliares.ERR_PEDIDO_NAO_PODE_SER_CANCELADO); 
                pedido.StatusPedido = NovoStatusPedido;
                
                await this._unityOfWork.PedidosRepository.Atualizar(PedidoID,pedido);
                
                await this._unityOfWork.SaveChangesAsync();
                
                return true;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("[SERVICE] Pedido não encontrado para alteração de status. PedidoID: {PedidoID}", PedidoID);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[SERVICE] Erro ao alterar status do pedido {PedidoID}. Mensagem: {Mensagem}", 
                    PedidoID, ex.Message);
                throw;
            }
        }
    }
}
