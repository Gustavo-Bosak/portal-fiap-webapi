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

**Justificativa:** o SQLite não exige instalação nem configuração de servidor, o que garante que o projeto seja reproduzível em qualquer máquina sem dependências externas. Basta executar o comando de migration para ter o banco pronto.

## Estratégia de Herança

A estratégia adotada é **TPC (Table Per Concrete Type)**.

Cada classe concreta (`Aluno`, `Professor`) possui sua própria tabela no banco de dados, contendo todas as colunas — incluindo as herdadas da classe abstrata `Pessoa`. Não existe uma tabela compartilhada `Pessoas`.

**Justificativa:** o TPC evita JOINs desnecessários entre tabelas de hierarquia, simplifica consultas e garante que cada tabela seja autocontida. Como `Pessoa` é abstrata e nunca será instanciada diretamente, não há necessidade de uma tabela para ela.

## Como Executar

```bash
# 1. Restaurar dependências
dotnet restore

# 2. Aplicar migrations e criar o banco de dados
dotnet ef database update --project PortalFiap.Infrastructure --startup-project PortalFIAP.API

# 3. Executar a API
dotnet run --project PortalFIAP.API

# 4. Acessar os endpoints
# Health check: https://localhost:{port}/health
# Alunos:      https://localhost:{port}/api/alunos
```

## Endpoints Disponíveis

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/health` | Health check da aplicação |
| GET | `/api/alunos` | Lista todos os alunos |
| GET | `/api/alunos/{id}` | Busca aluno por ID |
| GET | `/api/cursos` | Lista todos os cursos |
| GET | `/api/cursos/{id}` | Busca curso por ID |
| GET | `/api/turmas` | Lista todas as turmas |
| GET | `/api/turmas/{id}` | Busca turma por ID |

## Arquitetura

O projeto segue **Clean Architecture** com quatro camadas:

- **PortalFIAP.Domain** — Entidades, enums e classes base. Sem dependências externas. Contém as regras de validação nas próprias entidades (métodos `Definir*`).
- **PortalFiap.Infrastructure** — Camada de acesso a dados com Entity Framework Core e SQLite. Contém o `PortalFiapContext` e as configurações de mapeamento (`IEntityTypeConfiguration<T>`).
- **PortalFIAP.Application** — Interfaces e implementações dos serviços de aplicação. Responsável por orquestrar consultas ao banco via `PortalFiapContext`.
- **PortalFIAP.API** — Ponto de entrada da aplicação. Contém os controllers, configuração de DI, seed de dados e endpoints da API.

**Fluxo de dependência:** API → Application → Infrastructure → Domain
