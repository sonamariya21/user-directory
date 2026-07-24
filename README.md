# user-directory

ASP.NET Core 8 Web API for managing a user directory.

## Build

```bash
dotnet build backend/user-directory.sln
```



## Run

```bash
dotnet run --project backend/backend.csproj --launch-profile backend
```

Then open Swagger at `https://localhost:7189/swagger`.

### Visual Studio
1. Open `backend/user-directory.sln`
2. Right-click **backend** → **Set as Startup Project**
3. Select launch profile **backend** (or **https**)
4. Press **F5** (Swagger opens automatically)

## Unit tests (MSTest)

 in Visual Studio: open `backend/user-directory.sln`, then Test → Run All Tests.

The `backend/backend.Tests` project covers:
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
