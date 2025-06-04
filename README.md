# 🛫 Rota de Viagem - API .NET Core

API RESTful para cadastro e consulta de rotas aéreas, com cálculo da **rota mais barata entre dois aeroportos**, mesmo com múltiplas conexões.

---

## 📌 Funcionalidades

- CRUD completo de rotas (origem, destino e valor)
- Cálculo da melhor rota com menor custo (via grafo com conexões)
- Dados mantidos em memória (sem banco de dados)
- Seed automático de rotas
- Arquitetura modular (Class Libraries + IoC)
- Documentação com Swagger
- Testes unitários com xUnit

---

## 📦 Tecnologias

- .NET 8 Web API
- xUnit
- Swagger (Swashbuckle)
- IoC (Dependency Injection)
- Clean architecture com Domain, Application, Infrastructure

---

## Acesse o Swagger em:
👉 https://localhost:7012/swagger

--

## 🔍 Exemplos de uso

### ▶️ Cadastrar nova rota  
`POST /api/rota`

```json
{
  "origem": "GRU",
  "destino": "BRC",
  "valor": 10
}
```
### ▶️ Cadastrar nova rota  
`POST /api/rota`

```json
{
  "origem": "GRU",
  "destino": "BRC",
  "valor": 10
}
```

## 🧪 Testes
Execute os testes unitários com:

```bash
dotnet test RotaViagem.Tests
```

## 📋 Seed automático de rotas
Essas rotas são populadas automaticamente na memória ao iniciar a aplicação:

- GRU → BRC: 10
- BRC → SCL: 5
- GRU → CDG: 75
- GRU → SCL: 20
- GRU → ORL: 56
- ORL → CDG: 5
- SCL → ORL: 20

## 🧠 Estrutura do projeto

- RotaViagem.API            --> API Web com Swagger
- RotaViagem.Application    --> Serviços e lógica de rota
- RotaViagem.Domain         --> Entidades e interfaces
- RotaViagem.Infrastructure --> Repositório em memória e seed
- RotaViagem.IoC            --> Configuração de DI
- RotaViagem.Tests          --> Testes unitários com xUnit

