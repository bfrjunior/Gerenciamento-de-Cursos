# 📚 Gerenciamento de Cursos e Matrículas

> Sistema completo para gerenciamento de cursos, alunos e matrículas desenvolvido como avaliação técnica.

## 📋 Sobre o Projeto

Este projeto implementa um sistema completo de gerenciamento acadêmico com as seguintes funcionalidades:

- ✅ **Gestão de Cursos**: Criação, edição, listagem e exclusão de cursos
- ✅ **Gestão de Alunos**: Cadastro completo de alunos com validações
- ✅ **Sistema de Matrículas**: Controle de matrículas com regras de negócio
- ✅ **Relatórios**: Geração de relatórios de cursos e matrículas
- ✅ **Validações**: Sistema robusto de validação de dados
- ✅ **Testes Unitários**: Cobertura de testes para serviços e validadores

## 🛠️ Tecnologias Utilizadas

### Backend
- **.NET 8** - Framework principal
- **Entity Framework Core** - ORM com In-Memory Database
- **AutoMapper** - Mapeamento de objetos
- **FluentValidation** - Validação de dados
- **Swagger/OpenAPI** - Documentação da API
- **xUnit** - Framework de testes


### DevOps
- **Docker** - Containerização
- **Docker Compose** - Orquestração de containers
- **GitHub Actions** - CI/CD

## 🏗️ Arquitetura do Projeto

```
Gerenciamento-de-Cursos/
├── Gerenciamento-cursos/          # API Backend (.NET 8)
│   ├── Controllers/               # Controladores da API
│   ├── Services/                  # Lógica de negócio
│   ├── Repositories/              # Camada de dados
│   ├── Models/                    # Modelos de domínio
│   ├── Dto/                       # Data Transfer Objects
│   ├── Validators/                # Validadores FluentValidation
│   └── Data/                      # Contexto do banco de dados
├── Gerenciamento-cursos.Tests/    # Testes unitários
└── client-app/                    # Frontend React (não mostrado)
```

## ⚙️ Pré-requisitos

Para executar este projeto, você precisa ter instalado:

- **Docker Desktop** (recomendado)
- **Node.js 18+** e **npm/yarn** (para o frontend)
- **.NET 8 SDK** (opcional, para desenvolvimento)

## 🚀 Como Executar

### Opção 1: Docker Compose (Recomendado)

```bash
# Clone o repositório
git clone <url-do-repositorio>
cd Gerenciamento-de-Cursos
```

### Opção 2: Docker Manual

```bash
# Construir a imagem
lembre-se de estar dentro de : Gerenciamento-de-Cursos

docker build -t gerenciamento-cursos-backend -f Gerenciamento-cursos/Dockerfile .

# Executar o container
docker run -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_URLS=http://+:8080 gerenciamento-cursos-backend
```

### Opção 3: Execução Local (.NET)

```bash
# Navegar para o diretório da API
cd Gerenciamento-cursos

# Restaurar dependências
dotnet restore

# Executar a aplicação
dotnet run
```

## 🌐 URLs de Acesso

### Desenvolvimento Local
Via Docker:
- **API**: http://localhost:8080/api
- **Swagger**: http://localhost:8080/swagger
- **Health Check**: http://localhost:8080/health
### Localhost:
- API: https://localhost:7238/api
- Swagger:https://localhost:7238/swagger/index.html


### Produção
- **Frontend**: https://gerenciamento-matriculas.vercel.app/
- **API**: https://gerenciamento-de-cursos.onrender.com/api


## 🧪 Executando Testes

```bash
# Navegar para o diretório de testes
cd Gerenciamento-cursos.Tests

# Executar todos os testes
dotnet test

# Executar testes com cobertura
dotnet test --collect:"XPlat Code Coverage"
```

## 📊 Endpoints da API

### Cursos
- `GET /api/cursos` - Listar todos os cursos
- `GET /api/cursos/{id}` - Obter curso por ID
- `POST /api/cursos` - Criar novo curso
- `PUT /api/cursos/{id}` - Atualizar curso
- `DELETE /api/cursos/{id}` - Excluir curso

### Alunos
- `GET /api/alunos` - Listar todos os alunos
- `GET /api/alunos/{id}` - Obter aluno por ID
- `POST /api/alunos` - Criar novo aluno
- `PUT /api/alunos/{id}` - Atualizar aluno
- `DELETE /api/alunos/{id}` - Excluir aluno

### Matrículas
- `GET /api/matriculas` - Listar todas as matrículas
- `GET /api/matriculas/{id}` - Obter matrícula por ID
- `POST /api/matriculas` - Criar nova matrícula
- `DELETE /api/matriculas/{id}` - Cancelar matrícula

### Relatórios
- `GET /api/relatorios/cursos` - Relatório de cursos
- `GET /api/relatorios/matriculas` - Relatório de matrículas

## 🔧 Configurações

### Variáveis de Ambiente

```bash
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8080
```

### Banco de Dados

O projeto utiliza **Entity Framework Core In-Memory Database** para simplificar a execução e deploy. Os dados são populados automaticamente na inicialização da aplicação.

## 📈 Funcionalidades Implementadas

- [x] CRUD completo de Cursos
- [x] CRUD completo de Alunos
- [x] Sistema de Matrículas
- [x] Validações de negócio
- [x] Tratamento de erros
- [x] Documentação Swagger
- [x] Testes unitários
- [x] Containerização Docker
- [x] Deploy em produção
- [x] CI/CD com GitHub Actions



---

