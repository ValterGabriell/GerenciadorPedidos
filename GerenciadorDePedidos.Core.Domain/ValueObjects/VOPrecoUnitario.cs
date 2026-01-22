using GerenciadorDePedidos.Core.Domain.Validacoes;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDePedidos.Core.Domain.ValueObjects
{
    public record VOPrecoUnitario
    {
        public decimal _VOPrecoUnitario { get; init; }
        public VOPrecoUnitario() => _VOPrecoUnitario = 0;
        public VOPrecoUnitario(decimal PrecoUnitario)
        {
            this._VOPrecoUnitario = PrecoUnitario;
            PrecoUnitarioValidacoes regrasDeValidacoes = new();
            var resultado = regrasDeValidacoes.Validate(this);
            if (!resultado.IsValid)
            {
                var erros = string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validação falhou para VOPrecoUnitario: {erros}");
            }
        }
    }
}
