# Fluxo de entrega — CP3 e CP4

Entrega no portal do aluno: **somente o link do repositório GitHub** (um único integrante envia). Não enviar ZIP.

## 1. Checklist antes de entregar

```bash
dotnet build PortalFIAP.sln          # 0 erros
dotnet test                          # Domain + Application verdes
dotnet run --project PortalFIAP.API --launch-profile http
```

Com a API no ar (`http://localhost:5056`):

- [ ] `/swagger` lista os endpoints com descrições e respostas
- [ ] `GET /api/cursos` → 200 e `POST /api/cursos` → 201
- [ ] `PUT /api/cursos/{id inexistente}` → 404 `application/problem+json`
- [ ] `POST /api/cursos` com `cargaHoraria: 0` → 400 `application/problem+json`
- [ ] `GET /health` → 200 com os checks `self` e `database`
- [ ] Console mostra o `TraceId` nos logs de POST/PUT e no erro tratado

## 2. CP3 — critérios

| Critério | Onde |
|----------|------|
| API REST e Clean Architecture | `PortalFIAP.API/Controllers`, `PortalFIAP.Application/DTO` |
| Swagger completo | `PortalFIAP.API/Extensions/SwaggerExtensions.cs` |
| Repositório genérico | `IRepository<T>` (Application), `Repository<T>` (Infrastructure) |
| GlobalExceptionHandler | `PortalFIAP.API/Exceptions/GlobalExceptionHandler.cs` |

## 3. CP4 — critérios

| Critério | Onde |
|----------|------|
| Health checks | `PortalFIAP.API/Extensions/HealthCheckExtensions.cs` |
| Observabilidade | `TraceIdLoggingExtensions.cs`, logs nos serviços e no handler |
| Testes de Domain | `PortalFIAP.Domain.Tests` |
| Testes de Application | `PortalFIAP.Application.Tests` |

## 4. Evidências (`docs/evidencias/`)

- `health-200.json`, `health-503.json`
- `problemdetails-404.json`
- `log-traceid.txt`
- `dotnet-test.txt`
- *(recomendado)* print do Swagger UI — `docs/evidencias/swagger.png`

## 5. Publicação

```bash
git pull --rebase origin main
git push origin main
git tag -a cp4 -m "CP4 - Health checks, observabilidade e testes xUnit"
git push origin cp4
```

Conferir no GitHub: repositório **público**, README com integrantes/RM, e sem `*.db` nem credenciais commitados. Enviar no portal o link `https://github.com/<org>/portal-fiap-webapi`.
