using FluentValidation;
using GerenciadorDePedidos.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Domain.Validacoes
{
    public class ItemPedidoValidacoes : AbstractValidator<ItemPedido>
    {
        public ItemPedidoValidacoes()
        {
            RuleFor(item => item.Quantidade)
         .GreaterThan(0)
         .WithMessage(item => $"A quantidade do item '{item.ProdutoNome._VOProdutoNome}' deve ser maior que zero.");
        }
    }
}
