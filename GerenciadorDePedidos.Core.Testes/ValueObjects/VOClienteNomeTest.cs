using GerenciadorDePedidos.Core.Domain.ValueObjects;
using GerenciadorDePedidos.Core.Library.Constantes;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace GerenciadorDePedidos.Core.Testes.ValueObjects
{
    public class VOClienteNomeTest
    {
        [Fact]
        public void Deve_Criar_VOClienteNome_Valido()
        {
            var nome = "Cliente Válido";
            var vo = new VOClienteNome(nome);
            Assert.Equal(nome, vo._VOClienteNome);
        }

        [Fact]
        public void Nao_Deve_Criar_VOClienteNome_Vazio()
        {
            var ex = Assert.Throws<ValidationException>(() => new VOClienteNome(""));
            Assert.Contains(ConstantesAuxiliares.ERR_NOME_CLIENTE_VAZIO, ex.Message);
        }

 

        [Fact]
        public void Nao_Deve_Criar_VOClienteNome_Menor_Que_Quatro_Caracteres()
        {
            var ex = Assert.Throws<ValidationException>(() => new VOClienteNome("abc"));
            Assert.Contains(ConstantesAuxiliares.ERR_NOME_CLIENTE_TAMANHO_PEQUENO, ex.Message);
        }
    }
}
