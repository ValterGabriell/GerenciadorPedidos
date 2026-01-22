using GerenciadorDePedidos.Core.Domain;
using GerenciadorDePedidos.Core.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorDePedidos.Core.CrossCutting
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (context.Pedidos.Any())
            {
                return;
            }

            var itensPedido1 = new List<ItemPedido>
            {
                ItemPedido.CriarItemPedido("Notebook Dell Inspiron 15", 2, 3500.00m),
                ItemPedido.CriarItemPedido("Mouse Logitech MX Master 3", 3, 450.00m),
                ItemPedido.CriarItemPedido("Teclado Mecânico Keychron K2", 2, 650.00m)
            };

            var itensPedido2 = new List<ItemPedido>
            {
                ItemPedido.CriarItemPedido("Monitor LG UltraWide 29\"", 1, 1800.00m),
                ItemPedido.CriarItemPedido("Webcam Logitech C920", 2, 550.00m)
            };

            var itensPedido3 = new List<ItemPedido>
            {
                ItemPedido.CriarItemPedido("Headset HyperX Cloud II", 1, 450.00m),
                ItemPedido.CriarItemPedido("Mousepad Gamer RGB Grande", 2, 120.00m),
                ItemPedido.CriarItemPedido("Suporte para Notebook", 1, 85.00m)
            };

            var itensPedido4 = new List<ItemPedido>
            {
                ItemPedido.CriarItemPedido("SSD Samsung 1TB", 2, 550.00m),
                ItemPedido.CriarItemPedido("Memória RAM DDR4 16GB", 4, 350.00m)
            };

            var itensPedido5 = new List<ItemPedido>
            {
                ItemPedido.CriarItemPedido("Cadeira Gamer ThunderX3", 1, 1200.00m)
            };

            var pedido1 = Pedido.CriarPedido("João Silva", itensPedido1);
            var pedido2 = Pedido.CriarPedido("Maria Santos", itensPedido2);
            var pedido3 = Pedido.CriarPedido("Pedro Oliveira", itensPedido3);
            var pedido4 = Pedido.CriarPedido("Ana Costa", itensPedido4);
            var pedido5 = Pedido.CriarPedido("Carlos Souza", itensPedido5);

            foreach (var item in itensPedido1)
                item.PedidoId = pedido1.Id;

            foreach (var item in itensPedido2)
                item.PedidoId = pedido2.Id;

            foreach (var item in itensPedido3)
                item.PedidoId = pedido3.Id;

            foreach (var item in itensPedido4)
                item.PedidoId = pedido4.Id;

            foreach (var item in itensPedido5)
                item.PedidoId = pedido5.Id;

            context.Pedidos.AddRange(pedido1, pedido2, pedido3, pedido4, pedido5);
            context.ItensPedido.AddRange(itensPedido1);
            context.ItensPedido.AddRange(itensPedido2);
            context.ItensPedido.AddRange(itensPedido3);
            context.ItensPedido.AddRange(itensPedido4);
            context.ItensPedido.AddRange(itensPedido5);

            context.SaveChanges();
        }
    }
}
