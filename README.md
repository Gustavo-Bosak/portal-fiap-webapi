# Portal FIAP — Gestão Acadêmica

## Integrantes

| Nome | RM |
|-----|-----|
| Felipe Ferrete | RM: 562999 |
| Gustavo Bosak | RM: 566315 |
| Nikolas Brisola | RM: 564371 |

## Domínio

O sistema representa uma estrutura de **gestão acadêmica** para a FIAP, contemplando o cadastro e consulta de alunos, professores, cursos, turmas, matrículas, bolsas e endereços. O objetivo é modelar as entidades e seus relacionamentos, expondo os dados via uma Web API RESTful em .NET 10.

## SGBD

O banco de dados utilizado é o **SQLite**.

**Justificativa:** o SQLite não exige instalação nem configuração de servidor, o que garante que o projeto seja reproduzível em qualquer máquina sem dependências externas. As migrations são aplicadas automaticamente na inicialização da API (`MigrateAsync`), que também popula dados de exemplo se o banco estiver vazio.

## Estratégia de Herança

A estratégia adotada é **TPC (Table Per Concrete Type)**.

Cada classe concreta (`Aluno`, `Professor`) possui sua própria tabela no banco de dados, contendo todas as colunas — incluindo as herdadas da classe abstrata `Pessoa`. Não existe uma tabela compartilhada `Pessoas`.

**Justificativa:** o TPC evita JOINs desnecessários entre tabelas de hierarquia, simplifica consultas e garante que cada tabela seja autocontida. Como `Pessoa` é abstrata e nunca será instanciada diretamente, não há necessidade de uma tabela para ela.

## Como Executar

```bash
# 1. Restaurar dependências
dotnet restore

# 2. Executar a API (aplica as migrations e semeia dados de exemplo na primeira execução)
dotnet run --project PortalFIAP.API --launch-profile http
```

Com o perfil `http` a API sobe em `http://localhost:5056` (perfil `https`: `https://localhost:7117`). Em **Development** o banco é `app.db`; nos demais ambientes, `fiap.db`. Ambos ficam no diretório de execução e são ignorados pelo Git.

| Recurso | URL |
|---------|-----|
| **Swagger UI** | `http://localhost:5056/swagger` (apenas em Development) |
| Health check | `http://localhost:5056/health` |
| OpenAPI JSON | `http://localhost:5056/openapi/v1.json` (apenas em Development) |

Para aplicar as migrations manualmente (opcional): `dotnet ef database update --project PortalFiap.Infrastructure --startup-project PortalFIAP.API`

## Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/alunos` | Lista os alunos ativos |
| GET | `/api/alunos/{id}` | Busca aluno por ID |
| POST | `/api/alunos` | Cadastra um aluno |
| PUT | `/api/alunos/{id}` | Atualiza um aluno |
| DELETE | `/api/alunos/{id}` | Desativa um aluno (exclusão lógica) |
| GET | `/api/cursos` | Lista os cursos |
| GET | `/api/cursos/{id}` | Busca curso por ID |
| POST | `/api/cursos` | Cadastra um curso |
| PUT | `/api/cursos/{id}` | Atualiza um curso |
| DELETE | `/api/cursos/{id}` | Desativa um curso |
| GET | `/api/turmas` | Lista as turmas |
| GET | `/api/turmas/{id}` | Busca turma por ID |
| POST | `/api/turmas` | Cadastra uma turma (o curso precisa existir) |
| PUT | `/api/turmas/{id}` | Atualiza uma turma |
| DELETE | `/api/turmas/{id}` | Desativa uma turma |
| GET | `/health` | Health check (`self` + `database`) |

Enums são serializados pelo nome (ex.: `"AnaliseEDesenvolvimentoDeSistemas"`).

### Exemplos de chamada

```bash
# Sucesso: listar cursos
curl http://localhost:5056/api/cursos

# Sucesso: criar curso (201 Created)
curl -X POST http://localhost:5056/api/cursos \
  -H "Content-Type: application/json" \
  -d '{"nome":"EngenhariaDeSoftware","cargaHoraria":2400}'

# Erro tratado: id inexistente (404 application/problem+json)
curl -X PUT http://localhost:5056/api/cursos/00000000-0000-0000-0000-000000000001 \
  -H "Content-Type: application/json" \
  -d '{"nome":"EngenhariaDeSoftware","cargaHoraria":2400}'

# Erro tratado: payload inválido (400 application/problem+json)
curl -X POST http://localhost:5056/api/cursos \
  -H "Content-Type: application/json" \
  -d '{"nome":"EngenhariaDeSoftware","cargaHoraria":0}'
```

Exemplo de `ProblemDetails` (404):

```json
{
  "title": "Recurso não encontrado.",
  "status": 404,
  "detail": "Curso com id '00000000-0000-0000-0000-000000000001' não foi encontrado(a).",
  "instance": "/api/cursos/00000000-0000-0000-0000-000000000001",
  "traceId": "0HNONG86UCMOM:00000001"
}
```

## Arquitetura

O projeto segue **Clean Architecture** com quatro camadas:

- **PortalFIAP.Domain** — Entidades, enums, exceções de domínio e classes base. Sem dependências externas. Contém as regras de validação nas próprias entidades (métodos `Definir*`), que lançam `DomainException`.
- **PortalFiap.Infrastructure** — Acesso a dados com Entity Framework Core e SQLite: `PortalFiapContext`, configurações de mapeamento (`IEntityTypeConfiguration<T>`), migrations e repositórios.
- **PortalFIAP.Application** — Interfaces (`IRepository<T>`, repositórios e serviços), serviços de aplicação e DTOs. Não conhece o EF Core.
- **PortalFIAP.API** — Controllers enxutos, DI, Swagger, `GlobalExceptionHandler`, health checks, seed e logs. Os controllers **não** acessam o `DbContext`.

**Fluxo de dependência:** API → Application → Domain; Infrastructure → Application + Domain.

### Repositório genérico

`IRepository<T>` (Application) define o contrato para qualquer `BaseEntity`: `GetAllAsync`, `GetByIdAsync`, `ExistsByIdAsync`, `AddAsync`, `UpdateAsync` e `DeleteAsync`. A implementação `Repository<T>` (Infrastructure) usa `Context.Set<T>()`, `AsNoTracking` nas listagens e **exclusão lógica** (`Deactivate`), retornando apenas registros ativos. Está registrada na DI com `AddScoped(typeof(IRepository<>), typeof(Repository<>))`.

Repositórios específicos herdam de `Repository<T>` e sobrescrevem `Query()` para incluir relacionamentos (`CursoRepository`, `TurmaRepository`). O `AlunoRepository` é específico por causa do grafo de `Include` (endereço, matrículas, turma e bolsa). O fluxo de **Turmas** e **Cursos** usa o repositório genérico de ponta a ponta.

### Mapeamento de exceções (`GlobalExceptionHandler`)

Todas as respostas de erro seguem a **RFC 7807** (`application/problem+json`) e trazem o `traceId`. Fora de Development, erros inesperados retornam mensagem genérica, sem stack trace nem detalhes de banco (o detalhe fica no log).

| Exceção | Status HTTP |
|---------|-------------|
| `DomainException` | 400 Bad Request |
| `ArgumentException` / `ArgumentNullException` | 400 Bad Request |
| Payload malformado (`BadHttpRequestException` / `JsonException`) | 400 Bad Request |
| `ResourceNotFoundException` / `KeyNotFoundException` | 404 Not Found |
| `ConflictException` | 409 Conflict |
| Qualquer outra | 500 Internal Server Error |

## Health Checks (`GET /health`)

Endpoint único, com resposta JSON (status geral, duração e lista de checks).

| Check | O que verifica |
|-------|----------------|
| `self` | Processo no ar (sempre `Healthy`) |
| `database` | Conectividade com o SQLite, via `AddDbContextCheck<PortalFiapContext>` (`CanConnectAsync`) |

| Estado | HTTP |
|--------|------|
| Healthy | 200 |
| Degraded | 200 |
| Unhealthy | 503 |

O detalhe de exceção só aparece em Development. Evidências: `docs/evidencias/health-200.json` e `docs/evidencias/health-503.json` (banco inacessível, gerado com uma connection string inválida apenas local).

## Observabilidade (logs)

- Cada requisição abre um logger scope com o `TraceId` (`HttpContext.TraceIdentifier`) e devolve o header `X-Trace-Id`; o console exibe os scopes (`Logging:Console:IncludeScopes`).
- `AlunoService` e `CursoService` registram início e sucesso (ou aviso de recurso não encontrado) de criação, atualização e remoção, com propriedades nomeadas (ex.: `{CursoId}`).
- O `GlobalExceptionHandler` loga com o mesmo `traceId` (Warning para 4xx, Error para 500), que também vai no `ProblemDetails`.
- Exemplo: `docs/evidencias/log-traceid.txt`. Print do Swagger: `docs/evidencias/swagger-print.png`.

## Testes

```bash
dotnet test
```

| Projeto | Escopo |
|---------|--------|
| `PortalFIAP.Domain.Tests` | Regras do domínio, sem mock: `[Fact]` no caminho feliz e `[Theory]` no de erro (AAA) |
| `PortalFIAP.Application.Tests` | Serviços com **Moq** nos repositórios; falhas verificam `Times.Never`, sucessos `Times.Once` |

Resultado atual: 71 testes de Domain + 15 de Application, todos passando (`docs/evidencias/dotnet-test.txt`).

## Estrutura da solução

```
PortalFIAP.API                  Controllers, Program.cs, Extensions, Exceptions, Seed
PortalFIAP.Application          Interfaces, serviços, DTOs
PortalFiap.Domain               Entidades, enums, exceções de domínio
PortalFiap.Infrastructure       DbContext, configurações, migrations, repositórios
PortalFIAP.Domain.Tests         xUnit (sem mock)
PortalFIAP.Application.Tests    xUnit + Moq
docs/                           MER e evidências
```
