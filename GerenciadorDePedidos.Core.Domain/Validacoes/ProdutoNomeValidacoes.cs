using FluentValidation;
using GerenciadorDePedidos.Core.Domain.ValueObjects;
using GerenciadorDePedidos.Core.Library.Constantes;

namespace GerenciadorDePedidos.Core.Domain.Validacoes
{
    public class ProdutoNomeValidacoes : FluentValidation.AbstractValidator<VOProdutoNome>
    {
        public ProdutoNomeValidacoes()
        {
            RuleFor(produto => produto._VOProdutoNome)
                .NotEmpty().WithMessage(ConstantesAuxiliares.ERR_NOME_PRODUTO_VAZIO)
                .NotNull().WithMessage(ConstantesAuxiliares.ERR_NOME_PRODUTO_VAZIO)
                .Must(nome => nome != null && nome.Length > 1).WithMessage(ConstantesAuxiliares.ERR_NOME_PRODUTO_TAMANHO_PEQUENO);
        }
    }
}
