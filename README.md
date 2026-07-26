# user-directory

ASP.NET Core 8 Web API for managing a user directory.

Clean Architecture layout under `backend/`:

| Project | Role |
|---------|------|
| `UserDirectory.Domain` | Entities |
| `UserDirectory.Application` | Use cases, DTOs, ports |
| `UserDirectory.Infrastructure` | EF Core, JWT, repository implementations |
| `UserDirectory.Api` | Controllers, host, Swagger |
| `UserDirectory.Tests` | Unit tests |

## Build

```bash
dotnet build backend/user-directory.sln
```

## Run

```bash
dotnet run --project backend/UserDirectory.Api/UserDirectory.Api.csproj --launch-profile backend
```

Then open Swagger at `https://localhost:7189/swagger`.

### Visual Studio
1. Open `backend/user-directory.sln`
2. Right-click **UserDirectory.Api** → **Set as Startup Project**
3. Select launch profile **backend** (or **https**)
4. Press **F5** (Swagger opens automatically)

## Unit tests (MSTest)

```bash
dotnet test backend/user-directory.sln
```

Or in Visual Studio: open `backend/user-directory.sln`, then Test → Run All Tests.

The `UserDirectory.Tests` project covers:
- `UsersController` (API responses with mocked service)
- `UserService` (business logic with mocked repository)

## Auth (JWT Bearer)

Protected endpoints require:

```http
Authorization: Bearer <access_token>
```

### Local JWT (default)

`Jwt:Authority` is empty, so the API validates locally signed JWTs.

1. `POST /api/auth/login` with:
   ```json
   { "username": "admin", "password": "Admin@123" }
   ```
2. Copy `accessToken` from the response.
3. In Swagger click **Authorize**, paste the token (Swagger adds `Bearer `), then call user APIs.

### OAuth2 / OIDC (production IdP)

Set in `appsettings.json`:

```json
"Jwt": {
  "Authority": "https://YOUR_IDP/.well-known/openid-configuration-host",
  "Audience": "user-directory-api"
}
```

Examples:
- Azure AD: `https://login.microsoftonline.com/{tenant-id}/v2.0`
- Auth0: `https://{your-domain}.auth0.com/`

When `Authority` is set, local `/api/auth/login` is disabled — get tokens from the IdP.

## API

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| POST | `/api/auth/login` | No | Issue local JWT (dev) |
| GET | `/api/user-directory/get-all-users` | Yes | List users |
| GET | `/api/user-directory/get-user-by-id/{id}` | Yes | Get user |
| POST | `/api/user-directory/add-user` | Yes | Create user |
| PUT | `/api/user-directory/update-user/{id}` | Yes | Update user |
| DELETE | `/api/user-directory/delete-user/{id}` | Yes | Delete user |

## Cursor AI
Use Cursor’s AI chat to simplify **code implementation** and **Git** — write features faster and handle **pull**, **commit**, and **push**

## Default demo credentials

| Username | Password |
|----------|----------|
| `admin` | `Admin@123` |
