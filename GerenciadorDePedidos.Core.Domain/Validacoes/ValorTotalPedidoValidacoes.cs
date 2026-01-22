using FluentValidation;
using GerenciadorDePedidos.Core.Library.Constantes;
using GerenciadorDePedidos.Core.Library.ValueObjects;

namespace GerenciadorDePedidos.Core.Domain.Validacoes
{
    public class ValorTotalPedidoValidacoes : AbstractValidator<VOValorTotalPedido>
    {
        public ValorTotalPedidoValidacoes()
        {
            RuleFor(v => v._VOValorTotal)
                .GreaterThanOrEqualTo(0).WithMessage(ConstantesAuxiliares.ERR_VALOR_TOTAL_PEDIDO_NEGATIVO)
                .LessThanOrEqualTo(10_000_000).WithMessage(ConstantesAuxiliares.ERR_VALOR_TOTAL_PEDIDO_MUITO_ALTO)
                .Must(TemNoMaximoDuasCasasDecimais).WithMessage(ConstantesAuxiliares.ERR_VALOR_TOTAL_PEDIDO_MUITAS_CASAS_DECIMAIS);
        }

        private bool TemNoMaximoDuasCasasDecimais(decimal valor)
        {
            return decimal.Round(valor, 2) == valor;
        }
    }
}
