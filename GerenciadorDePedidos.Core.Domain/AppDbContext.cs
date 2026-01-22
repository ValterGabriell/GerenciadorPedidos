using GerenciadorDePedidos.Core.Domain.Models;
using GerenciadorDePedidos.Core.Domain.ValueObjects;
using GerenciadorDePedidos.Core.Library.Constantes;
using GerenciadorDePedidos.Core.Library.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GerenciadorDePedidos.Core.Domain
{
    public sealed class AppDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public AppDbContext(IConfiguration configuration) => this._config = configuration;


        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbName = _config[ConstantesAuxiliares.NOME_BANCO_DADOS_IN_MEMORY];
            if (!string.IsNullOrEmpty(dbName))
                optionsBuilder.UseInMemoryDatabase(dbName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(pedido =>
            {
                pedido.HasKey(p => p.Id);
                pedido.HasIndex(p => p.DataCriacao);
                pedido.HasIndex(p => p.StatusPedido);

                pedido.Property(p => p.ClienteNome)
                      .HasConversion(
                          v => v._VOClienteNome,
                          v => new VOClienteNome { _VOClienteNome = v });

                pedido.Property(p => p.ValorTotal)
                      .HasConversion(
                          v => v._VOValorTotal,
                          v => new VOValorTotalPedido { _VOValorTotal = v });

                pedido.HasMany<ItemPedido>()
                      .WithOne()
                      .HasForeignKey(ip => ip.PedidoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ItemPedido>(itemPedido =>
            {
                itemPedido.HasKey(ip => ip.Id);
                itemPedido.HasIndex(ip => ip.PedidoId);

                itemPedido.Property(ip => ip.ProdutoNome)
                          .HasConversion(
                              v => v._VOProdutoNome,
                              v => new VOProdutoNome { _VOProdutoNome = v });

                itemPedido.Property(ip => ip.PrecoUnitario)
                          .HasConversion(
                              v => v._VOPrecoUnitario,
                              v => new VOPrecoUnitario { _VOPrecoUnitario = v });
            });
        }
    }
}
