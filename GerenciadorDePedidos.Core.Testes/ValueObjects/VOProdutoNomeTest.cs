using GerenciadorDePedidos.Core.Domain.ValueObjects;
using GerenciadorDePedidos.Core.Library.Constantes;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace GerenciadorDePedidos.Core.Testes.ValueObjects
{
    public class VOProdutoNomeTest
    {
        [Fact]
        public void Deve_Criar_VOProdutoNome_Valido()
        {
            var nome = "Produto Válido";
            var vo = new VOProdutoNome(nome);
            Assert.Equal(nome, vo._VOProdutoNome);
        }

        [Fact]
        public void Nao_Deve_Criar_VOProdutoNome_Vazio()
        {
            var ex = Assert.Throws<ValidationException>(() => new VOProdutoNome(""));
            Assert.Contains(ConstantesAuxiliares.ERR_NOME_PRODUTO_VAZIO, ex.Message);
        }

        [Fact]
        public void Nao_Deve_Criar_VOProdutoNome_Nulo()
        {
            var ex = Assert.Throws<ValidationException>(() => new VOProdutoNome(null));
            Assert.Contains(ConstantesAuxiliares.ERR_NOME_PRODUTO_VAZIO, ex.Message);
        }

 
    }
}
