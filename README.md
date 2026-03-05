# CP1 — Modelo Entidade-Relacionamento (MER) e Estrutura WebAPI (.NET)

## Integrantes do Grupo

| Nome | RM |
|-----|-----|
| Felipe Ferrete | RM: 562999 |
| Gustavo Bosak | RM: 566315 |
| Nikolas Brisola | RM: 564371 |

---

# Domínio do Projeto

O domínio escolhido foi **Portal FIAP (Gestão Acadêmica)**.

O sistema representa uma estrutura simplificada de gestão acadêmica contendo informações sobre:

- Pessoas da instituição
- Alunos
- Professores
- Cursos
- Turmas
- Matrículas
- Bolsas acadêmicas
- Endereço das pessoas

O objetivo é demonstrar **modelagem de entidades e relacionamentos** antes da implementação de banco de dados ou regras de negócio completas.

---

# Modelo Entidade-Relacionamento (MER)

O diagrama MER encontra-se na pasta:

```
/docs/mer.png
```

O modelo apresenta:

- **Entidades**
- **Atributos principais**
- **Chaves primárias (PK)**
- **Relacionamentos**
- **Cardinalidade**
- **Opcionalidade**

---

# Entidades Modeladas

As seguintes entidades foram modeladas no projeto:

| Entidade | Descrição |
|------|------|
| BaseEntity | Classe base com propriedades comuns das entidades |
| Pessoa | Classe abstrata com dados básicos de uma pessoa |
| Aluno | Representa um aluno matriculado na instituição |
| Professor | Representa um professor |
| Curso | Cursos oferecidos pela instituição |
| Turma | Turmas vinculadas a um curso |
| Matricula | Registro de vínculo entre aluno e turma |
| Endereco | Endereço associado a uma pessoa |
| Bolsa | Bolsa de estudo associada a um aluno |

---

# Relacionamentos

Resumo dos relacionamentos modelados no MER:

| Relacionamento | Tipo | Descrição |
|---|---|---|
Pessoa → Endereco | 1:1 | Uma pessoa possui um endereço |
Matricula → Bolsa | 1:1 (Opcional) | Uma matricula pode possuir uma bolsa |
Aluno → Matricula | 1:N | Um aluno pode possuir várias matrículas |
Turma → Matricula | 1:N | Uma turma possui várias matrículas |
Curso → Turma | 1:N | Um curso pode possuir várias turmas |
Professor → Turma | N:N | Professores podem possuir múltiplas turmas |

---

# Estratégia de Identificação

Todas as entidades utilizam **GUID como chave primária (PK)**.

Exemplo:

```csharp
public Guid Id { get; private set; }
```

Essa abordagem foi escolhida para garantir **identificadores únicos e independentes de banco de dados**.

---

# Estrutura do Repositório

```
/
├── docs
│   └── mer.png
│
├── src
│   └── Projeto.Api
│
└── README.md
```

Descrição:

| Pasta | Conteúdo |
|------|------|
| docs | Contém o diagrama MER |
| src | Estrutura inicial do projeto WebAPI |
| README | Documentação do projeto |

---

# Objetivo Acadêmico

Este projeto foi desenvolvido para praticar:

- Modelagem de dados
- Criação de MER
- Estruturação de entidades em C#
- Organização de projeto .NET
- Relacionamentos entre entidades

---

# Referência

> “Faça o teu melhor, na condição que você tem, enquanto você não tem condições melhores, para fazer melhor ainda.”

**Mario Sergio Cortella**
