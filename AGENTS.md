# WordPicker — Agent Guide

## Quick start

```bash
dotnet build
dotnet test
dotnet run --project src/WordPicker.Web
```

Frontend: Bootstrap 5 + Alpine.js via CDN only (no npm, no bundler). No authentication — all pages are public.

No CI, no linter/formatter config, no pre-commit hooks. No global.json — SDK version is whatever `dotnet --version` returns (targets `net10.0`).

## Project layout

- `src/WordPicker.Web/` — ASP.NET Core Razor Pages app (not MVC)
- `tests/WordPicker.Tests/` — MSTest test project (not xUnit/NUnit)
- `WordPicker.slnx` — new `.slnx` format (not traditional `.sln`)
- `openspec/` — spec-driven development docs (not consumed at build time)

## Database

| Environment | Provider  | Connection string (dev)                                 |
|-------------|-----------|--------------------------------------------------------|
| Dev/Prod    | PostgreSQL| `Host=localhost;Database=WordPicker;...` (in `appsettings.Development.json`) |
| Tests       | SQLite in-memory | via `TestDbContextFactory.Create()`               |

Run migrations with `dotnet ef migrations add <name> --project src/WordPicker.Web`.

## Tests

```bash
dotnet test                              # all tests
dotnet test --filter "TestCategory=Unit" # unit tests only
dotnet test tests/WordPicker.Tests       # single project
```

- Framework: MSTest (`[TestClass]`/`[TestMethod]`, not xUnit)
- Test classes inherit `BaseTest` which provides `CreateContext()` (SQLite in-memory, short-lived per test)
- Tests run in parallel at method level by default
- Test categories: `TestCategories.Unit` / `TestCategories.Integration`
- Test naming: `MethodName_When<Scenario>_Then<Result>`
- Assertions: built-in `Assert` class only (no Fluent Assertions)
- Every feature/fix must include unit tests with SQLite in-memory database

## Architecture notes

- Domain models: `WordList` (has many `WordEntry` items) — see `src/WordPicker.Web/Models/`
- EF Core 10 with Npgsql; fluent config in `ApplicationDbContext.OnModelCreating`
- Home page (`/`) redirects to `/WordLists/Index`
- Word list detail page uses Razor Pages handler methods: `OnPostAddWordAsync`, `OnPostRemoveWordAsync`
- Duplicate word entries prevented by unique composite index on `(WordListId, Value)`
- `appsettings.Development.json` is gitignored (contains dev DB password)

## Coding conventions

- **No Repository Pattern** — use EF Core `DbContext` directly in page models
- **No shared logic** — each page model owns its own queries and logic; no services, helpers, or shared utilities
- **Only query what you need with EF Core** — Never do an entire ToList for example, always only select the columns you need.
- **Never query inside a loop** — Keep the query to the edges of the code.
- **Keep performance in mind** — Don't use `FirstOrDefault` in a loop when you don't have to and create a dictionary beforehand.
- **No additional projects** for functional code (only the web project and test project)
- **No npm** — all client-side assets (Bootstrap, Alpine.js) loaded via CDN
- **All client-side interactivity uses Alpine.js** — no vanilla JS for DOM manipulation, no jQuery, no Stimulus, no htmx
- **Never use Third Party code of which the Nuget package hasn't been installed** — Ask for permission before installing and using
