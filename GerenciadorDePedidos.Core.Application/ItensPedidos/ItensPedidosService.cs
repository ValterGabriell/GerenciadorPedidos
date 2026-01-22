using GerenciadorDePedidos.Core.Application.UnityOfWork;
using GerenciadorDePedidos.Core.Domain.Models;
using GerenciadorDePedidos.Core.Domain.Validacoes;
using GerenciadorDePedidos.Core.Domain.ValueObjects;
using GerenciadorDePedidos.Core.Dto.ItensPedidoDTO;
using GerenciadorDePedidos.Core.Library.Constantes;
using GerenciadorDePedidos.Core.Library.Filtros;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GerenciadorDePedidos.Core.Application.ItensPedidos
{
    public sealed class ItensPedidosServiceImpl : IItensPedidosService
    {
        private readonly IUnityOfWork _unityOfWork;

        public ItensPedidosServiceImpl(IUnityOfWork unityOfWork)
        {
            this._unityOfWork = unityOfWork;
        }

        public async Task<Guid> AdicionarItemPedido(DtoCriarItemPedido dtoCriarItemPedido)
        {
            var itemPedidoParaCriar = ItemPedido.CriarItemPedido(
                dtoCriarItemPedido.ProdutoNome, 
                dtoCriarItemPedido.Quantidade, 
                dtoCriarItemPedido.PrecoUnitario,
                dtoCriarItemPedido.PedidoID
            );
            this._unityOfWork.ItensPedidoRepository.Adicionar(itemPedidoParaCriar);
            await this._unityOfWork.SaveChangesAsync();
            return itemPedidoParaCriar.Id;
        }

        public async Task AdicionarVariosItensPedido(IEnumerable<DtoCriarItemPedido> dtosCriarItensPedido)
        {
            var itensParaCriar = dtosCriarItensPedido.Select(dto =>
            {
                var item = ItemPedido.CriarItemPedido(dto.ProdutoNome, dto.Quantidade, dto.PrecoUnitario, dto.PedidoID);
                return item;
            }).ToList();
            this._unityOfWork.ItensPedidoRepository.AdicionarVariosItensPedido(itensParaCriar);
            await this._unityOfWork.SaveChangesAsync();
        }

        public async Task<DtoListarItensPedido?> ObterItemPedidoPorIdAsync(Guid id)
        {
            var itemPedido = await this._unityOfWork.ItensPedidoRepository.RecuperarPorId(id);
            if (itemPedido == null)
                return null;
            return new DtoListarItensPedido(
                itemPedido.Id,
                itemPedido.PedidoId,
                itemPedido.ProdutoNome._VOProdutoNome,
                itemPedido.Quantidade,
                itemPedido.PrecoUnitario._VOPrecoUnitario
            );
        }

        public async Task<(IEnumerable<DtoListarItensPedido> ItensPedido, int Count)> ListarItensPedidoAsync(IEnumerable<FiltrosDinamicos> filtros, int pageNumber, int pageSize)
        {
            var resultado = await this._unityOfWork.ItensPedidoRepository.RecuperaLista(filtros, pageNumber, pageSize);
            var itensDto = resultado.Data.Select(item => new DtoListarItensPedido(
                item.Id,
                item.PedidoId,
                item.ProdutoNome._VOProdutoNome,
                item.Quantidade,
                item.PrecoUnitario._VOPrecoUnitario
            ));
            return (itensDto, resultado.TotalCount);
        }

        public async Task AtualizarItemPedido(Guid PedidoID,DtoAtualizarItemPedido dtoAtualizarItemPedido)
        {
            var itemExistente = await this._unityOfWork.ItensPedidoRepository.RecuperarPorId(dtoAtualizarItemPedido.Id);
            itemExistente.ValidarQuantidadeProduto();
           
            if (itemExistente.PedidoId != PedidoID) throw new KeyNotFoundException(ConstantesAuxiliares.ERR_ITEM_NAO_ESTA_NO_PEDIDO);
            if (itemExistente != null)
            {
                itemExistente.ProdutoNome = new VOProdutoNome(dtoAtualizarItemPedido.ProdutoNome);
                itemExistente.Quantidade = dtoAtualizarItemPedido.Quantidade;
                itemExistente.PrecoUnitario = new VOPrecoUnitario(dtoAtualizarItemPedido.PrecoUnitario);
                await this._unityOfWork.ItensPedidoRepository.Atualizar(dtoAtualizarItemPedido.Id,itemExistente);
                await this._unityOfWork.SaveChangesAsync();
            }
        }

        public async Task RemoverItemPedido(Guid id)
        {
            await this._unityOfWork.ItensPedidoRepository.Remover(id);
            await this._unityOfWork.SaveChangesAsync();
        }
    }
}
