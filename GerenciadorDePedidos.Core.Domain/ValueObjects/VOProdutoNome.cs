using GerenciadorDePedidos.Core.Domain.Validacoes;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDePedidos.Core.Domain.ValueObjects
{
    public record VOProdutoNome
    {
        public string _VOProdutoNome { get; init; }
        public VOProdutoNome() => _VOProdutoNome = string.Empty;
        public VOProdutoNome(string ProdutoNome)
        {
            this._VOProdutoNome = ProdutoNome;
            ProdutoNomeValidacoes regrasDeValidacoes = new();
            var resultado = regrasDeValidacoes.Validate(this);
            if (!resultado.IsValid)
            {
                var erros = string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validação falhou para VOProdutoNome: {erros}");
            }
        }
    }
}
