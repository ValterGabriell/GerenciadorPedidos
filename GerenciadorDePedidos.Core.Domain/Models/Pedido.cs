using GerenciadorDePedidos.Core.Domain.ValueObjects;
using GerenciadorDePedidos.Core.Library.Constantes;
using GerenciadorDePedidos.Core.Library.Enum;
using GerenciadorDePedidos.Core.Library.ValueObjects;

namespace GerenciadorDePedidos.Core.Domain.Models
{
    
    public sealed class Pedido 
    {
        private Pedido() { }

        public Guid Id { get; set; }
        public VOClienteNome ClienteNome { get; set; } = new VOClienteNome();
        public DateTime DataCriacao { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public VOValorTotalPedido ValorTotal { get; set; } = new VOValorTotalPedido();

        public ICollection<ItemPedido> ItensDoPedido { get; set; } = [];


        public static Pedido CriarPedido(string ClienteNome, ICollection<ItemPedido> ItensDoPedido)
        {
            if (ItensDoPedido.Count <= 0) throw new ArgumentException(ConstantesAuxiliares.ERR_PEDIDO_SEM_ITENS);
            return new Pedido
            {
                Id = Guid.NewGuid(),
                ClienteNome = new VOClienteNome(ClienteNome),
                DataCriacao = DateTime.UtcNow,
                StatusPedido = StatusPedido.Novo,
                ValorTotal = new VOValorTotalPedido(ItensDoPedido.Sum(item => item.PrecoUnitario._VOPrecoUnitario * item.Quantidade)),
                ItensDoPedido = ItensDoPedido
            };
        }

        public static Pedido CriarPedido(string ClienteNome)
        {
            return new Pedido
            {
                Id = Guid.NewGuid(),
                ClienteNome = new VOClienteNome(ClienteNome),
                DataCriacao = DateTime.UtcNow,
                StatusPedido = StatusPedido.Novo
            };
        }
        
        public void CalcularValorTotalPedido()
        {
            if (ItensDoPedido.Count <= 0) throw new ArgumentException(ConstantesAuxiliares.ERR_PEDIDO_SEM_ITENS);
            this.ValorTotal = new VOValorTotalPedido(this.ItensDoPedido.Sum(item => item.PrecoUnitario._VOPrecoUnitario * item.Quantidade));
        }
        public bool PedidoEstaPago() => this.StatusPedido == StatusPedido.Pago;

    }
}
