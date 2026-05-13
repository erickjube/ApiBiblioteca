# API Biblioteca

API REST desenvolvida em ASP.NET Core para gerenciamento de uma biblioteca, com foco em controle de empréstimos, vendas e gestão de exemplares.

O projeto foi construído com ênfase em organização de código, separação de responsabilidades e aplicação de boas práticas no desenvolvimento de APIs.

---

## Tecnologias

* ASP.NET Core
* Entity Framework Core
* SQL Server
---

## Bibliotecas auxiliares

* FluentValidation
* AutoMapper
* ASP.NET Core Identity
---

## Como executar

### Pré-requisitos

* .NET 8
* SQL Server

### Passos

```bash
### 1. Clonar o repositório:
git clone https://github.com/erickjube/ApiBiblioteca.git


### 2. Entrar na pasta do projeto:
cd ApiBiblioteca


### 3. Restaurar dependências:
dotnet restore


### 4. Configurar a Connection String:
No arquivo:

ApiBliblioteca.API/appsettings.json

configure a string de conexão do SQL Server:

json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=ApiBiblioteca;Trusted_Connection=True;TrustServerCertificate=True;"
}


### 5. Aplicar as migrations:
dotnet ef database update --project ApiBiblioteca.Infrastructure\ApiBiblioteca.Infrastructure.csproj --startup-project ApiBliblioteca.API\ApiBiblioteca.API.csproj


### 6. Compilar o projeto:
dotnet build ApiBliblioteca.API\ApiBiblioteca.API.csproj


### 7. Executar a API:
dotnet run --project ApiBliblioteca.API\ApiBiblioteca.API.csproj


## Usuário administrador padrão:
Ao iniciar a aplicação pela primeira vez, um usuário administrador é criado automaticamente.

Usuário: admin
Senha: Admin@123

```

## Endpoints principais

### Empréstimos

Responsável pelo controle de empréstimos, devoluções e geração de multas.

* POST /api/Emprestimo → realizar empréstimo
* POST /api/Emprestimo/{emprestimoId}/item
* POST /api/Emprestimo/{emprestimoId}/finalizar → finalizar empréstimo
* PATCH /api/Emprestimo/{emprestimoId}/itens-devolucao
* GET /api/Emprestimo/{id} → detalhes do empréstimo
* GET /api/Emprestimo/{emprestimoId}/multas → multas de um empréstimo 

### Vendas

* POST /api/Venda → registrar venda
* POST /api/Venda/{vendaId}/adicionar-item
* POST /api/Venda/{vendaId}/finalizar → finalizar venda
* GET /api/Venda/{vendaId}
* GET /api/Venda/{vendaId}/itens

### Clientes

* GET /api/Cliente
* POST /api/Cliente

### Exemplares

* GET /api/Exemplar
* POST /api/exemplar

### Autenticação

* POST /api/Auth/login
* POST /api/Auth/register
* POST /api/Auth/refresh-token

---

## Arquitetura

O projeto segue uma estrutura em camadas:

* **Controllers** → recebem requisições HTTP
* **Services** → regras de negócio
* **Repositories** → acesso a dados
* **DTOs** → transporte de dados entre camadas

Objetivo: manter separação de responsabilidades e facilitar manutenção e evolução do sistema.

---

## Principais entidades

* Livro
* Cliente
* Empréstimo
* Venda
* Multa
* Exemplar

---

## Funcionalidades

* Autenticação com JWT
* Validação de dados com FluentValidation
* Paginação de resultados
* Logging da aplicação
* Controle de empréstimos e devoluções
* Registro de multas após atraso no empréstimo
* Registro de vendas

---

## Melhorias futuras

* Adicionar testes automatizados
* Melhorar regras de negócio (ex: multas e prazos)
* Versionamento da API
* Separar a API em ambientes (dev / prod)
* Melhorar organização e desacoplamento das camadas

---

## Observações

Este projeto foi desenvolvido com fins de estudo e prática de desenvolvimento de APIs REST, servindo como base para evolução técnica em arquitetura e boas práticas.
