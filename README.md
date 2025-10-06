# Gerenciamento de Cursos

Este projeto � um backend para gerenciamento de cursos, alunos e matr�culas, desenvolvido com .NET 8 e SQL Server. Est� pronto para execu��o em containers Docker, incluindo aplica��o autom�tica de migrations.

## Tecnologias Utilizadas

- .NET 8 (C# 12)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Docker & Docker Compose
- Swagger (OpenAPI)

## Como Executar com Docker

1. **Clone o reposit�rio:**
```bash
git clone https://github.com/seuusuario/seurepositorio.git
cd seurepositorio
```

2. **Suba os containers:**
```bash
docker-compose up -d
```

3. **Acesse a API:**
- Acesse [http://localhost:5000/swagger](http://localhost:5000/swagger) para testar os endpoints.

## Configura��o de Banco de Dados

- O banco de dados � criado automaticamente via migrations.
- Connection string para Docker:

## Desenvolvimento Local

- Configure a connection string em `appsettings.json` para apontar para seu SQL Server local.
- Execute o projeto normalmente pelo Visual Studio ou CLI:

## Principais Endpoints

- `/api/alunos` - Gerenciamento de alunos
- `/api/cursos` - Gerenciamento de cursos
- `/api/matriculas` - Gerenciamento de matr�culas
- `/api/relatorios` - Relat�rios diversos

Consulte o Swagger para detalhes completos dos endpoints e modelos.

## Migrations

As migrations s�o aplicadas automaticamente ao iniciar o container. 
Para criar novas migrations localmente:

## Observa��es

- O projeto utiliza CORS para permitir acesso do frontend em `http://localhost:5173` e `https://gerenciamento-matriculas.vercel.app`.
- O Swagger est� habilitado em ambiente de desenvolvimento.

---

**Pronto para avalia��o:**  
Basta executar `docker-compose up --build` e acessar o Swagger para testar a aplica��o.