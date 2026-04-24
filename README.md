# 📚 Biblioteca Comunitária API

API desenvolvida em **.NET 5** para gestão de **livros** e **empréstimos** em bibliotecas comunitárias.

O projeto segue princípios de **Domain-Driven Design (DDD)**, com separação clara entre camadas e foco em **domínio rico**.

---

## 🚀 Tecnologias Utilizadas

* .NET 5
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* FluentValidation
* Swagger (OpenAPI)

---

## 🏗️ Arquitetura

O projeto foi estruturado seguindo boas práticas de separação de responsabilidades:

```text
src/
 ├── Biblioteca.Api            → Camada de apresentação (Controllers, Middlewares)
 ├── Biblioteca.Application    → Casos de uso, DTOs e serviços
 ├── Biblioteca.Domain         → Regras de negócio e entidades
 └── Biblioteca.Infrastructure → Persistência, EF Core, repositórios

tests/
 ├── Biblioteca.UnitTests
 └── Biblioteca.IntegrationTests
```

### 📌 Camadas

* **Domain** → contém entidades e regras de negócio
* **Application** → orquestra os casos de uso
* **Infrastructure** → acesso a dados com EF Core
* **API** → exposição REST dos endpoints

---

## 📖 Regras de Negócio

### Livro

* Não pode ser criado sem título ou autor
* Quantidade disponível não pode ser negativa
* Reduz quantidade ao realizar empréstimo
* Aumenta quantidade ao devolver

### Empréstimo

* Só pode ser criado se houver estoque disponível
* Não pode ser devolvido duas vezes

---

## 🌐 Endpoints

### 📚 Livros

| Método | Endpoint         |
| ------ | ---------------- |
| POST   | /api/livros      |
| GET    | /api/livros      |
| GET    | /api/livros/{id} |

---

### 🔄 Empréstimos

| Método | Endpoint                        |
| ------ | ------------------------------- |
| POST   | /api/emprestimos                |
| PATCH  | /api/emprestimos/{id}/devolucao |
| GET    | /api/emprestimos                |
| GET    | /api/emprestimos/{id}           |

---

## ▶️ Como Executar o Projeto

### 1. Clonar o repositório

```bash
git clone <URL_DO_REPOSITORIO>
cd biblioteca
```

---

### 2. Restaurar dependências

```bash
dotnet restore
```

---

### 3. Configurar banco de dados

Edite o arquivo:

```text
src/Biblioteca.Api/appsettings.Development.json
```

Exemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=biblioteca_db;Username=postgres;Password=postgres"
}
```

---

### 4. Aplicar migrations

```bash
dotnet ef database update \
--project src/Biblioteca.Infrastructure \
--startup-project src/Biblioteca.Api
```

---

### 5. Executar a aplicação

```bash
dotnet run --project src/Biblioteca.Api
```

---

### 6. Acessar Swagger

```text
https://localhost:xxxx/swagger
```

---

## 🧪 Como Testar a API

### Criar Livro

```http
POST /api/livros
```

```json
{
  "titulo": "Clean Code",
  "autor": "Robert C. Martin",
  "anoPublicacao": 2008,
  "quantidadeDisponivel": 3
}
```

---

### Criar Empréstimo

```http
POST /api/emprestimos
```

```json
{
  "livroId": "GUID_DO_LIVRO"
}
```

---

### Devolver Empréstimo

```http
PATCH /api/emprestimos/{id}/devolucao
```

---

## ⚙️ Tratamento de Erros

A API possui middleware global para tratamento de exceções.

Exemplo de resposta:

```json
{
  "status": 404,
  "error": "NotFound",
  "message": "Livro não encontrado.",
  "timestamp": "2026-01-01T00:00:00Z"
}
```

---

## 🧠 Decisões Técnicas

* Utilização de **DDD pragmático** para separar responsabilidades
* **Domínio rico**, com regras dentro das entidades
* Uso de **FluentValidation** para validação de entrada
* Uso de **Middleware global** para tratamento de erros
* Separação entre **Application e Infrastructure**
* Uso de **EF Core com PostgreSQL**

---

## 📌 Pontos de Evolução

* Logging estruturado
* Autenticação e autorização

---

## 👨‍💻 Autor - Eraldo Carlos

Projeto desenvolvido como avaliação técnica de backend.
