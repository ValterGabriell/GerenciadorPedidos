# 🛒 Gerenciador de Pedidos - WizCO

API RESTful para gerenciamento de pedidos desenvolvida em .NET 8 com foco em Domain-Driven Design (DDD) e Clean Architecture.

---

## 📖 Sobre o Projeto

O **Gerenciador de Pedidos** é uma API desenvolvida para gerenciar pedidos de forma eficiente, permitindo criar, consultar, atualizar e deletar pedidos e seus respectivos itens. O projeto foi construído seguindo princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**, garantindo código limpo, testável e escalável.

### ✨ Funcionalidades

- ✅ Criação de pedidos com validação de dados
- ✅ Gerenciamento de itens de pedido
- ✅ Cálculo automático do valor total
- ✅ Validação de regras de negócio (Value Objects)
- ✅ Tratamento centralizado de exceções
- ✅ Logging estruturado
- ✅ Documentação Swagger/OpenAPI
- ✅ Testes unitários com xUnit

---

## 🚀 Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **C# 12** - Linguagem de programação
- **Entity Framework Core** - ORM para acesso a dados
- **xUnit** - Framework de testes
- **FluentValidation** - Validação de objetos
- **Swagger/OpenAPI** - Documentação da API
- **Serilog/ILogger** - Logging estruturado

---

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture** e está organizado em camadas:


### Camadas

- **GerenciadorPedidos (API)**: Controllers, Middleware, Program.cs
- **Core.Application**: Serviços de aplicação, casos de uso, interfaces
- **Core.Domain**: Entidades, Value Objects, validações de domínio
- **Core.Dto**: Objetos de transferência de dados
- **Core.Library**: Enums, constantes, utilitários
- **Core.CrossCutting**: Injeção de dependência, configurações
- **Core.Testes**: Testes unitários

---

## ⚙️ Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) ou superior
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

---

## 📦 Instalação

### 1. Clone o repositório

### 2. Restaure as dependências

### 3. Execute o projeto

---


### Cobertura de Testes

O projeto inclui testes para:
- ✅ Validação de Value Objects
- ✅ Regras de negócio de Pedidos
- ✅ Regras de negócio de Itens de Pedido
- ✅ Cálculo de valor total
- ✅ Validação de quantidade de produtos

---


## 🎯 Padrões Implementados

### Domain-Driven Design (DDD)

- **Entities**: Pedido, ItemPedido
- **Value Objects**: VOClienteNome, VOProdutoNome, VOPrecoUnitario, VOValorTotalPedido
- **Aggregates**: Pedido como raiz de agregação
- **Repository Pattern**: Acesso a dados encapsulado

### Clean Architecture

- **Separação de Camadas**: Domain, Application, Infrastructure, Presentation
- **Dependency Inversion**: Interfaces na camada de domínio
- **Use Cases**: Serviços de aplicação desacoplados

### Outros Padrões

- **Unit of Work**: Gerenciamento de transações
- **Factory Method**: Métodos estáticos `CriarPedido()`, `CriarItemPedido()`
- **Validation Pattern**: FluentValidation para regras de negócio
- **DTO Pattern**: Separação entre modelos de domínio e transporte


---

## 👤 Autor

**Valter Gabriel Brito da Silva**

- Email: valtergabrielbs@protonmail.com
- GitHub: [@ValterGabriell](https://github.com/ValterGabriell)



## 📝 Notas Adicionais

### Status do Pedido

O sistema trabalha com os seguintes status:

- `Novo` - Pedido recém-criado
- `Pago` - Pedido confirmado e pago
- `Cancelado` - Pedido cancelado (apenas se não estiver pago)

### Validações Implementadas

- ✅ Nome do cliente não pode ser vazio
- ✅ Pedido deve ter pelo menos 1 item
- ✅ Quantidade de produto deve ser maior que zero
- ✅ Preço unitário deve ser maior que zero
- ✅ Pedido pago não pode ser cancelado
- ✅ Valor total calculado automaticamente


**Desenvolvido usando .NET 8 e Clean Architecture**



