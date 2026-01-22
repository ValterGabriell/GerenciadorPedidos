using FluentValidation;
using GerenciadorDePedidos.Core.Domain.ValueObjects;
using GerenciadorDePedidos.Core.Library.Constantes;

namespace GerenciadorDePedidos.Core.Domain.Validacoes
{
    public class PedidoValidacoes : AbstractValidator<VOClienteNome>
    {
        public PedidoValidacoes()
        {
            RuleFor(clienteNome => clienteNome._VOClienteNome).NotEmpty().WithMessage(ConstantesAuxiliares.ERR_NOME_CLIENTE_VAZIO);
            RuleFor(clienteNome => clienteNome._VOClienteNome).NotNull().WithMessage(ConstantesAuxiliares.ERR_NOME_CLIENTE_VAZIO);
            RuleFor(clienteNome => clienteNome._VOClienteNome).Must(e => e.Length > 3).WithMessage(ConstantesAuxiliares.ERR_NOME_CLIENTE_TAMANHO_PEQUENO);
        }
    }

}
