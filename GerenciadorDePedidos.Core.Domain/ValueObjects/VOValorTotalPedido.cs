using GerenciadorDePedidos.Core.Domain.Validacoes;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDePedidos.Core.Library.ValueObjects
{
    public record VOValorTotalPedido
    {
        public decimal _VOValorTotal { get; init; }
        public VOValorTotalPedido(decimal ValorTotal)
        {
            this._VOValorTotal = ValorTotal;
            ValorTotalPedidoValidacoes regrasDeValidacoes = new();
            var resultado = regrasDeValidacoes.Validate(this);
            if (!resultado.IsValid)
            {
                var erros = string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validação falhou para VOValorTotalPedido: {erros}");
            }
        }
        public VOValorTotalPedido() => this._VOValorTotal = 0;

    }
}
