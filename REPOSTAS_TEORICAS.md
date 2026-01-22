### **Lógica / Arquitetura**

**1) Você tem uma API lenta. Como investigaria o problema?**

Eu analisaria métricas de performance, logs para identificar onde o tempo está sendo gasto, verificando se o gargalo está no código, no banco de dados, em chamadas externas ou na infraestrutura. Também avaliaria consumo de CPU, memória e queries lentas no banco.

**2) Endpoint muito chamado, sem parâmetros e query pesada. Como melhorar?**

Como o resultado é sempre o mesmo, a principal solução é aplicar cache em memória ou distribuído para evitar múltiplas execuções da query pesada.

**3) Como garantir que uma operação crítica não seja executada duas vezes?**

Isso pode ser garantido usando idempotência, chaves únicas, locks ou restrições no banco de dados, as filas também ajudam a garantir execução única da operação.

**4) Como lidar com concorrência em atualização de dados?**

Eu utilizaria controle de concorrência otimista com versionamento de registros, evitando locks sempre que possível. 

**5) O que fazer se um endpoint receber 10x mais tráfego?**

Aplicaria cache, rate limiting e avaliaria escala horizontal da aplicação. Também consideraria filas para processamento assíncrono e otimização de consultas.

**6) Endpoint dá erro de CORS no browser mas funciona no Postman. Por quê?**

Porque o browser aplica a política de CORS e exige headers específicos que o Postman não valida. Normalmente o problema está na configuração incorreta de CORS.

---

### **.NET / C#**

**1) Diferença entre Task, ValueTask e async void**

Task é o padrão para operações assíncronas, ValueTask é usado em cenários específicos para reduzir alocações quando o resultado muitas vezes é síncrono, e async void deve ser evitado, sendo indicado apenas para event handlers.

**2) Dependency Injection no .NET Core e lifetimes**

Dependency Injection é um padrão que desacopla dependências e facilita testes e manutenção. Transient cria uma nova instância sempre, Scoped cria uma instância por request e Singleton mantém uma única instância para toda a aplicação.

**3) O que é Middleware no ASP.NET Core?**

Middleware é um componente do pipeline que intercepta requisições e respostas, podendo executar lógica antes ou depois da próxima etapa, como autenticação, logging, CORS e tratamento de erros.

---

### **Dapper**

**1) Quando escolher Dapper em vez de EF Core?**

A escolha do Dapper se dá quando precisa de alta performance, controle total do SQL ou consultas complexas, especialmente em cenários de leitura pesada.

**2) Como evitar SQL Injection no Dapper?**

Utilizando sempre parâmetros nomeados em vez de concatenar strings SQL, garantindo que os valores sejam tratados corretamente pelo banco e evitando execução de código malicioso.

---

### **REST API**

**1) Diferença entre PUT e PATCH**

PUT substitui o recurso inteiro, enquanto PATCH atualiza apenas parte do recurso, sendo mais indicado para alterações parciais.

**2) Como versionar uma API?**

A forma mais comum é versionar pela URL, como `/v1/resource`, mas também é possível usar headers ou media types específicos.

**3) Códigos HTTP comuns e seus usos**

200 indica sucesso, 201 criação de recurso, 400 erro de requisição, 401 falta de autenticação, 403 falta de permissão, 404 recurso não encontrado e 500 erro interno do servidor.

---

### **Banco de Dados**

**1) Diferença entre INNER JOIN e LEFT JOIN**

INNER JOIN retorna apenas registros com correspondência nas duas tabelas, enquanto LEFT JOIN retorna todos os registros da tabela da esquerda, mesmo sem correspondência na direita, podendo trazer valores nulos.

**2) O que é índice? Quando pode piorar a performance?**

Índice é uma estrutura que acelera consultas, mas pode piorar a performance quando há muitas escritas, índices desnecessários ou baixa seletividade dos dados.

**3) Diferença entre banco relacional e não relacional**

Bancos relacionais usam tabelas e relações bem definidas, como SQL Server e PostgreSQL, enquanto não relacionais priorizam flexibilidade e escala, como MongoDB e Redis.

