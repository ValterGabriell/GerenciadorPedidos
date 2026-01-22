using FluentValidation;
using GerenciadorDePedidos.Core.Domain.ValueObjects;
using GerenciadorDePedidos.Core.Library.Constantes;

namespace GerenciadorDePedidos.Core.Domain.Validacoes
{
    public class PrecoUnitarioValidacoes : AbstractValidator<VOPrecoUnitario>
    {
        public PrecoUnitarioValidacoes()
        {
            RuleFor(preco => preco._VOPrecoUnitario)
                .GreaterThan(0).WithMessage(ConstantesAuxiliares.ERR_PRECO_UNITARIO_MENOR_OU_IGUAL_ZERO)
                .LessThanOrEqualTo(1_000_000).WithMessage(ConstantesAuxiliares.ERR_PRECO_UNITARIO_MUITO_ALTO)
                .Must(TemNoMaximoDuasCasasDecimais).WithMessage(ConstantesAuxiliares.ERR_PRECO_UNITARIO_MUITAS_CASAS_DECIMAIS);
        }

        private bool TemNoMaximoDuasCasasDecimais(decimal preco)
        {
            return decimal.Round(preco, 2) == preco;
        }
    }
}
