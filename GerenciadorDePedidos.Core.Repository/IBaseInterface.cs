using GerenciadorDePedidos.Core.Library.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Repository
{
    public interface IBaseInterface<TEntidade> where TEntidade : class
    {
        TEntidade Adicionar(TEntidade entidade);
        int CriarVarios(List<TEntidade> entidade);
        Task<TEntidade?> Remover(Guid id);
        Task<TEntidade?> Atualizar(Guid id, TEntidade entidadeAtualizada);
        Task<TEntidade> RecuperarPorId(Guid id);
        Task<(IEnumerable<TEntidade> Data, int TotalCount)> RecuperaLista(
           IEnumerable<FiltrosDinamicos> filtros,
           int pageNumber,
           int pageSize);
    }
}
