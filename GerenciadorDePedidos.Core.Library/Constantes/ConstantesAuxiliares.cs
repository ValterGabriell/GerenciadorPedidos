using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDePedidos.Core.Library.Constantes
{
    public static class ConstantesAuxiliares
    {
        public const string NOME_BANCO_DADOS_IN_MEMORY = "Database:InMemoryDatabaseName";

        #region DtoValidacoes
        public const string ERR_PEDIDO_SEM_ITENS = "Um pedido deve conter ao menos um item.";
        public const string ERR_NOME_CLIENTE_VAZIO = "O nome do cliente não pode ser vazio.";
        public const string ERR_NOME_CLIENTE_TAMANHO_PEQUENO = "O nome do cliente deve conter ao menos 3 caracteres.";
        public const string ERR_PEDIDO_NAO_ENCONTRADO = "Pedido não encontrado.";
        public const string ERR_PEDIDO_NAO_PODE_SER_CANCELADO = "Pedido está pago e não pode ser cancelado.";
        public const string ERR_PRECO_UNITARIO_MENOR_OU_IGUAL_ZERO = "O preço unitário deve ser maior que zero.";
        public const string ERR_PRECO_UNITARIO_MUITO_ALTO = "O preço unitário não pode ser maior que 1.000.000.";
        public const string ERR_PRECO_UNITARIO_MUITAS_CASAS_DECIMAIS = "O preço unitário deve ter no máximo duas casas decimais.";
        public const string ERR_NOME_PRODUTO_VAZIO = "O nome do produto não pode ser vazio.";
        public const string ERR_NOME_PRODUTO_TAMANHO_PEQUENO = "O nome do produto deve conter ao menos 1 caracter.";
        public const string ERR_VALOR_TOTAL_PEDIDO_NEGATIVO = "O valor total do pedido não pode ser negativo.";
        public const string ERR_VALOR_TOTAL_PEDIDO_MUITO_ALTO = "O valor total do pedido não pode ser maior que 10.000.000.";
        public const string ERR_VALOR_TOTAL_PEDIDO_MUITAS_CASAS_DECIMAIS = "O valor total do pedido deve ter no máximo duas casas decimais.";
        public const string ERR_ITEM_NAO_ESTA_NO_PEDIDO = "ItemPedido não encontrado para o PedidoID fornecido.";
        #endregion
    }
}
