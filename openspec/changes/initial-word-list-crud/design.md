## Context

WordPicker is a new application requiring a web-based interface for managing word lists. This is the initial implementation establishing the foundational architecture and CRUD capabilities for word lists and their entries.

## Goals / Non-Goals

**Goals:**
- Establish ASP.NET Core MVC project structure with standard conventions
- Implement responsive UI with Bootstrap for styling and Alpine.js for interactive components
- Create persistent storage for word lists and words using PostgreSQL via Entity Framework Core
- Provide intuitive CRUD operations for word lists and word entries

**Non-Goals:**
- User authentication/authorization (future enhancement)
- Word definitions, translations, or linguistic features
- Import/export functionality
- API endpoints (MVC views only for now)

## Decisions

### Technology Stack

| Decision | Choice | Rationale |
|----------|--------|-----------|
| Backend Framework | ASP.NET Core MVC | Mature, well-documented, strong EF Core integration |
| Frontend Styling | Bootstrap 5 | Rapid responsive UI development, widely supported |
| Client Interactivity | Alpine.js | Lightweight, jQuery-free interactivity for form enhancements |
| ORM | Entity Framework Core | Native .NET integration, migration support |
| Database | PostgreSQL | Robust, free, excellent EF Core support via Npgsql |
| Project Structure | Standard MVC (Controllers/Views/Models) | Convention-based, IDE-friendly |

### Data Model

```
WordList
├── Id (int, PK)
├── Name (string, required, max 200 chars)
├── CreatedAt (datetime)
└── UpdatedAt (datetime)

WordEntry
├── Id (int, PK)
├── WordListId (int, FK -> WordList)
├── Value (string, required, max 100 chars)
└── CreatedAt (datetime)
```

### Architecture Decisions

- **No Repository Pattern**: Use EF Core DbContext directly in controllers for simplicity in this initial phase; never ever use repository patterns
- **Server-Side Rendering**: MVC views with Razor, Alpine.js for interactivity only (e.g., delete confirmations, form validation feedback)
- **Migrations**: EF Core Code-First migrations for database schema management
- **Projects**: No additional projects other than the MVC project should be made for functional code

### Testing Decisions

| Decision | Choice | Rationale |
|----------|--------|-----------|
| Test Framework | MSTest v4 | Modern, built-in .NET support, no extra dependencies |
| Database | SQLite in-memory | Fast test isolation, no external dependencies, mirrors EF Core usage |
| Assertions | MSTest built-in Assert class | No Fluent Assertions; keep assertion library minimal |
| Naming Convention | `MethodName_When<scenario>_Then<result>` | Clear, consistent, describes intent |

- **Every feature/fix MUST include unit tests** with SQLite in-memory database
- **Arrange-Act-Assert** pattern for all test methods
- Use `TestInitialize` for shared setup, `[TestCleanup]` for disposal

### Configuration Management

| Decision | Choice | Rationale |
|----------|--------|-----------|
| Sensitive Config Location | `appsettings.Development.json` | Keeps secrets out of source control |
| Template in Source | `appsettings.json` (empty values) | Documents required config keys without exposing values |
| Source Control | `appsettings.Development.json` in `.gitignore` | Prevents accidental credential leaks |

- **All environment-specific config** (connection strings, OAuth credentials, API keys) SHALL go in `appsettings.Development.json`
- **`appsettings.json` SHALL mirror all keys** but keep values empty as placeholders
- **`appsettings.Development.json` SHALL be in `.gitignore`** to prevent leaking secrets
- **Never hardcode** configuration values in source code

## Risks / Trade-offs

- **No authentication**: Lists are publicly accessible; acceptable for initial prototype but must be addressed before production use
- **EF Core in controllers**: Tight coupling acceptable for initial simplicity; refactor to services layer if business logic grows
- **Alpine.js learning curve**: Minimal; Alpine is intentionally simple and declarative
- **PostgreSQL dependency**: Requires local PostgreSQL installation; could use SQLite for development if needed
