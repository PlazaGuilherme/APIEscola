## API Escola — Guia de Execução

### Requisitos

- Docker e Docker Compose
- .NET SDK 8.0

### 1) Subir o banco de dados com Docker

O projeto inclui `docker-compose.yml` com SQL Server:

```bash
docker compose up -d
```

Detalhes do banco:
- Host: localhost
- Porta: 8001 (mapeada para 1433 no contêiner)
- Usuário: SA
- Senha: mF2xXyAU9T85D7KZ8sgLXxG9x

Aguarde ~10-20s até o SQL Server estar pronto.

### 2) Configurar a conexão no app (se necessário)

Por padrão `APIEscola/appsettings.json` já aponta para o SQL exposto no Docker:

```
ConnectionStrings:SqlServer = Server=localhost,8001;Database=APIEscolaDb;User Id=SA;Password=...;TrustServerCertificate=true;
```

Se desejar, altere usuário/senha/porta e mantenha o mesmo valor em `docker-compose.yml` e no `appsettings.json`.

### 3) Criar/Atualizar o banco (migrations)

Se precisar aplicar as migrations manualmente:

```bash
dotnet tool restore
dotnet ef database update --project APIEscola.Infrastructure --startup-project APIEscola
```

### 4) Rodar a API

```bash
dotnet restore
dotnet build
dotnet run --project APIEscola
```

Aplicação disponível em:
- Swagger: http://localhost:5263/swagger (porta pode variar conforme seu `launchSettings.json`)

### 5) Obter o Bearer Token (JWT)

O sistema é administrativo: apenas usuários administradores autenticados acessam as rotas.

Passo a passo via Swagger:
1. Acesse `/swagger`
2. Use `POST /api/usuario/registro` para criar um admin (exemplo de payload):

```json
{
  "nome": "Administrador",
  "cpf": "12345678900",
  "dataNascimento": "1990-01-01T00:00:00",
  "email": "admin@escola.com",
  "password": "Senha123!",
  "isAdmin": true
}
```

3. Faça login em `POST /api/usuario/login` com email/senha:

```json
{
  "email": "admin@escola.com",
  "password": "Senha123!"
}
```

4. A resposta conterá `token`. Clique em "Authorize" no Swagger e informe:

```
Bearer SEU_TOKEN_AQUI
```

Agora todas as rotas protegidas estarão habilitadas.

Exemplo com cURL:

```bash
# Login e captura do token (requer jq)
TOKEN=$(curl -s -X POST "http://localhost:5263/api/usuario/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@escola.com","password":"Senha123!"}' | jq -r '.token')

# Chamada autenticada
curl -H "Authorization: Bearer $TOKEN" http://localhost:5263/api/alunos
```

### 6) Configurações de JWT

Definidas em `APIEscola/appsettings.json` e `APIEscola/appsettings.Development.json`:

```
Jwt:Key, Jwt:Issuer, Jwt:Audience, Jwt:ExpirationHours
```

Em produção, altere `Jwt:Key` para uma chave forte e única.

### 7) Estrutura e Fluxo do Sistema

- Autenticação e autorização via JWT; política `AdminOnly` aplicada aos controllers: `Alunos`, `Turmas`, `Matricula` e endpoints autenticados de `Usuario`.
- Endpoints públicos: `POST /api/usuario/registro` e `POST /api/usuario/login`.
- Domínios e persistência:
  - `APIEscola.Domain`: entidades (ex.: `Usuario`, `Aluno`, `Turma`, `Matricula`)
  - `APIEscola.Infrastructure`: `AppDbContext`, repositórios e migrations (SQL Server)
  - `APIEscola.Application`: comandos/consultas (MediatR), validações e serviços (ex.: JWT)
  - `APIEscola` (API): controllers, middleware e configuração

### 8) Troubleshooting

- Erro de conexão com banco: verifique se o contêiner está ativo (`docker ps`) e se a `ConnectionStrings:SqlServer` aponta para `localhost,8001`.
- 401 Unauthorized: verifique se o token está no header `Authorization: Bearer <token>` e se não expirou.
- 403 Forbidden: apenas administradores acessam as rotas (registre com `isAdmin: true`).
- Certificado SQL: `TrustServerCertificate=true` já está definido para ambiente local.

### 9) Segurança (produção)

- Use HTTPS e uma `Jwt:Key` forte.
- Considere rotacionar chaves e reduzir `Jwt:ExpirationHours`.
- Avalie refresh tokens, rate limiting e auditoria de logins.



