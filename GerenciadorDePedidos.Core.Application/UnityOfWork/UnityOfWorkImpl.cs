using GerenciadorDePedidos.Core.Domain;
using GerenciadorDePedidos.Core.Repository.ItensPedidoRepository;
using GerenciadorDePedidos.Core.Repository.PedidosRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Application.UnityOfWork
{
    public sealed class UnityOfWorkImpl : IUnityOfWork
    {

        public IPedidosRepository PedidosRepository { get; }
        public IItensPedidoRepository ItensPedidoRepository { get; }
        public AppDbContext AppDbContext { get; }

        public UnityOfWorkImpl(IPedidosRepository pedidosRepository, IItensPedidoRepository itensPedidoRepository, AppDbContext appDbContext)
        {
            this.PedidosRepository = pedidosRepository;
            this.ItensPedidoRepository = itensPedidoRepository;
            this.AppDbContext = appDbContext;
        }

        public async Task<int> SaveChangesAsync() => await this.AppDbContext.SaveChangesAsync();

    }
}
