using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Library.Filtros
{
    public enum TipoFiltroDinamico
    {
        Igual = 1,
        Diferente = 2,
        Maior = 3,
        Menos = 4,
        Contem = 5
    }


    /// <summary>
    /// Representa um critério de filtro dinâmico para consultar entidades, especificando o nome da propriedade, o valor de comparação e o tipo de comparação.
    /// </summary>
    /// <remarks>Use este registro para definir filtros dinamicamente ao criar consultas em classes de entidades.
    /// Cada instância descreve uma única condição de filtro, que pode ser combinada com outras para construir uma lógica de consulta complexa.
    // O filtro é aplicado comparando a propriedade especificada da entidade com o valor fornecido usando o
    // tipo de comparação indicado.</remarks>
    public record FiltrosDinamicos
    {
        /// <summary>
        /// O nome da propriedade da classe de entidade!
        /// </summary>
        public string NomePropriedade { get; set; } = string.Empty;

        /// <summary>
        /// Valor dessa propriedade para comparação!
        /// </summary>
        public object? ValorPropriedade { get; set; } = null;

        /// <summary>
        /// Igualdade a ser utilizada na comparação!
        /// </summary>
        public TipoFiltroDinamico TipoDeIgualdade { get; set; } = TipoFiltroDinamico.Igual;
    }

}
