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
    public interface IUnityOfWork
    {
        IPedidosRepository PedidosRepository { get; }
        IItensPedidoRepository ItensPedidoRepository { get; }
        AppDbContext AppDbContext { get; }

        Task<int> SaveChangesAsync();
    }
}
