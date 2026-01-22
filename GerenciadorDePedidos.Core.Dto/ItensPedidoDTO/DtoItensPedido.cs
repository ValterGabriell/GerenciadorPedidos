using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Dto.ItensPedidoDTO
{
    public record DtoItensPedido();

    public record DtoCriarItemPedido(string ProdutoNome, int Quantidade, decimal PrecoUnitario, Guid PedidoID);
    public record DtoAtualizarItemPedido(Guid Id, string ProdutoNome, int Quantidade, decimal PrecoUnitario);
    public record DtoListarItensPedido(Guid Id, Guid PedidoId, string ProdutoNome, int Quantidade, decimal PrecoUnitario);
}
