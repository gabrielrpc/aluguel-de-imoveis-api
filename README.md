# Sistema de Aluguel de Imóveis

API de gestão de aluguel de imóveis, desenvolvida em ASP.NET Core (.NET 9) e SQL Server.

## Funcionalidades

- Cadastro e autenticação de usuários (JWT)
- Gerenciamento de imóveis e locações
- Armazenamento seguro de senhas (BCrypt)
- Validação de dados (FluentValidation)
- Documentação da API com Swagger
- Acesso a banco de dados via Entity Framework Core (SQL Server)

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

## Como executar

1. **Clone o repositório:**
```bash
   git clone repo-url
   cd aluguel-de-imoveis-api
```
   
2. **Restaure as dependências:**
```bash
   dotnet restore
```
   
3. **Configure a string de conexão:** No arquivo `launchSettings.json` (Caso não altere, o banco será criado em: (localdb)\mssqllocaldb).

4. **Execute a aplicação:** Ao executar a aplicação, o banco de dados será criado baseado na sua string de conexão e executará a migration inicial do projeto assim como a inclusão dos seeders para testes.

## Estrutura do Projeto
```bash
aluguel-de-imoveis/
  ├── Communication/
  │   ├── Request/            # Modelos de entrada da API
  │   └── Response/           # Modelos de saída da API
  ├── Controllers/            # Controllers da aplicação
  ├── Exceptions/             # Tratamento de exceções customizadas
  ├── Infraestructure/        # Contexto do EF Core e Seeders
  ├── Migrations/             # Armazena a migration inicial para criação do banco
  │   ├── DataAccess/         # Acesso ao context
  ├── Models/                 # Entidades de domínio
  ├── Repository/
  │   ├── Interfaces/         # Contratos de repositórios
  │   └── Implementations/    # Implementações dos repositórios
  ├── Security/               # Geração de token JWT, autenticação
  ├── Services/               # Serviços de regra de negócio
  │   ├── Interfaces/         # Contratos de serviços
  │   └── Validations/        # Validações com FluentValidation
  ├── Utils/                  # Classes de Utilidade e Enums
      └── Enums/              # Armazena dados do tipo enum
  └── Program.cs              # Arquivo de inicialização
```

## Principais Dependências

- BCrypt.Net-Next
- FluentValidation
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.EntityFrameworkCore (SQL Server)
- Swashbuckle.AspNetCore (Swagger)
---
