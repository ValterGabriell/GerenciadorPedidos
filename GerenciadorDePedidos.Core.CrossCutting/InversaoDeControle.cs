using GerenciadorDePedidos.Core.Application.ItensPedidos;
using GerenciadorDePedidos.Core.Application.Pedidos;
using GerenciadorDePedidos.Core.Application.UnityOfWork;
using GerenciadorDePedidos.Core.Domain;
using GerenciadorDePedidos.Core.Repository.ItensPedidoRepository;
using GerenciadorDePedidos.Core.Repository.PedidosRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.CrossCutting
{
    public static class InversaoDeControle
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddRepositories();
            services.AddServices();
            services.AddUnityOfWork();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPedidosRepository, PedidosRepositoryImpl>();
            services.AddScoped<IItensPedidoRepository, ItensPedidoRepositoryImpl>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPedidosService, PedidoServiceImpl>();
            services.AddScoped<IItensPedidosService, ItensPedidosServiceImpl>();
            return services;
        }

        private static IServiceCollection AddUnityOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWorkImpl>();
            return services;
        }
    }
}

