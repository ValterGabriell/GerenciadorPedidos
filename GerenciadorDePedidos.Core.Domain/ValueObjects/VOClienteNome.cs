using GerenciadorDePedidos.Core.Domain.Validacoes;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDePedidos.Core.Domain.ValueObjects
{
    /// <summary>
    /// Representa o value object para o nome de um cliente.
    /// </summary>
    public record VOClienteNome
    {
        public string _VOClienteNome { get; init; }
        public VOClienteNome(string ClienteNome)
        {
            this._VOClienteNome = ClienteNome;
            PedidoValidacoes regrasDeValidacoes = new();
            var resultado = regrasDeValidacoes.Validate(this);
            if (!resultado.IsValid)
            {
                var erros = string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validação falhou para VOClienteNome: {erros}");
            }
        }

        public VOClienteNome() => this._VOClienteNome = string.Empty;
    }
}
