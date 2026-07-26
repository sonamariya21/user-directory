# User Directory — Frontend

React + TypeScript UI for the User Directory API. Users can log in with JWT, view the user list, and add new users.

## Stack

- React 19
- TypeScript
- Vite 8
- React Router
- Axios
- React Hook Form

## Prerequisites

- Node.js 18+ (or current LTS)
- Backend API running at `https://localhost:7189` (see root / `backend` README)

## Setup

```bash
cd frontend
npm install
```

## Environment

Create or edit `.env`:

```env
VITE_API_URL=/api
```

In development, Vite proxies `/api` to the backend (`https://localhost:7189`). See `vite.config.ts`.

## Run

```bash
npm run dev
```

Open the URL Vite prints (usually `http://localhost:5173`).

### Other scripts

| Command | Description |
|---------|-------------|
| `npm run build` | Typecheck + production build |

## Pages

| Path | Auth | Description |
|------|------|-------------|
| `/login` | No | Sign in and store JWT |
| `/` | Yes | User list |
| `/add` | Yes | Add user form |

Protected routes redirect to `/login` when no token is present.

## Demo login

| Username | Password |
|----------|----------|
| `admin` | `Admin@123` |

(Same credentials as the backend local JWT login.)

## Project structure

```text
frontend/
├── src/
│   ├── components/     # NavBar, ProtectedRoute
│   ├── pages/          # Login, UserList, AddUser
│   ├── services/       # Axios API + auth storage
│   ├── types/          # Shared TypeScript types
│   ├── validations/    # Form validation helpers
│   ├── App.tsx
│   └── main.tsx
├── .env
└── vite.config.ts
```
