using GerenciadorDePedidos.Core.Domain;
using GerenciadorDePedidos.Core.Domain.Models;
using GerenciadorDePedidos.Core.Library.Extensoes;
using GerenciadorDePedidos.Core.Library.Filtros;
using GerenciadorDePedidos.Core.Repository.PedidosRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Repository
{
    public abstract class BaseRepository<TEntidade> : IBaseInterface<TEntidade> where TEntidade : class
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PedidosRepositoryImpl> _logger;

        protected BaseRepository(AppDbContext context, ILogger<PedidosRepositoryImpl> logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual TEntidade Adicionar(TEntidade entidade)
        {
            _logger.LogInformation($"Adicionando entidade: {entidade.GetType}");
            ArgumentNullException.ThrowIfNull(entidade);
            var entidadeAdicionada = _context.Set<TEntidade>().Add(entidade).Entity;
            return entidadeAdicionada;
        }

        public virtual int CriarVarios(List<TEntidade> entidade)
        {
            _logger.LogInformation($"Adicionando range de entidade: {entidade.GetType}");
            ArgumentNullException.ThrowIfNull(entidade);
            _context.Set<TEntidade>().AddRange(entidade);
            return entidade.Count;
        }

        public virtual async Task<TEntidade?> Remover(Guid id)
        {
            TEntidade? entidade = await RecuperarPorId(id);
            if (entidade != null)
                _context.Set<TEntidade>().Remove(entidade);

            return entidade;
        }

        public virtual async Task<TEntidade?> Atualizar(Guid id, TEntidade entidadeAtualizada)
        {
            if (entidadeAtualizada == null) throw new ArgumentNullException(nameof(entidadeAtualizada));
            
            var entidadeExistente = await _context.Set<TEntidade>()
                .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
            
            if (entidadeExistente == null)
                throw new KeyNotFoundException($"Entidade {typeof(TEntidade).Name} com ID: {id} não encontrada.");

            _context.Entry(entidadeExistente).CurrentValues.SetValues(entidadeAtualizada);
            
            return entidadeExistente;
        }

        public virtual async Task<TEntidade> RecuperarPorId(Guid id)
        {
            var tipoEntidade = typeof(TEntidade);

            var entidade = await _context.Set<TEntidade>()
                  .FirstOrDefaultAsync(e =>
                      EF.Property<Guid>(e, "Id") == id);

            return entidade ?? throw new KeyNotFoundException(
                $"Entidade {tipoEntidade} com ID: {id} não encontrada.");
        }

        public virtual async Task<(IEnumerable<TEntidade> Data, int TotalCount)> RecuperaLista(
           IEnumerable<FiltrosDinamicos> filtros,
           int pageNumber,
           int pageSize)
        {
            var query = this._context.Set<TEntidade>().AsNoTracking().AsQueryable();
            query = query.AplicarFiltroDinamico(filtros);

            var totalCount = await query.CountAsync();

            var data = await query
                .PaginacaoNoBanco(pageNumber, pageSize)
                .ToListAsync();

            return (data, totalCount);
        }



    }
}
