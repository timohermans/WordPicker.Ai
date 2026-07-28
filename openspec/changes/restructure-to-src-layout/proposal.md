## Why

The project root is cluttered with source code, project files, build artifacts, and configuration mixed together without structure. This makes it hard to navigate and will only get worse as the project grows (Dockerfiles, CI configs, potential frontend apps). A standard `src/` + `tests/` layout with a proper solution file establishes a clean foundation for future growth.

## What Changes

- Create `WordPicker.sln` at repo root referencing both projects
- Move main web project from repo root to `src/WordPicker.Web/`
- Rename project from `WordPicker.csproj` to `WordPicker.Web.csproj`
- Rename namespace from `WordPicker.*` to `WordPicker.Web.*`
- Move test project from repo root to `tests/WordPicker.Tests/`
- Delete and regenerate EF Core migrations to absorb namespace change
- Delete stale `bin/` and `obj/` directories

## Capabilities

### New Capabilities

_(none — this change is structural only, no new capabilities)_

### Modified Capabilities

_(none — spec-level behavior does not change)_

## Impact

- **BREAKING**: All namespaces change from `WordPicker.*` to `WordPicker.Web.*` — every `.cs` and `.cshtml` file in the web project is affected
- **BREAKING**: Project paths change — the test `.csproj` project reference and any tooling pointing at `WordPicker.csproj` must be updated
- **BREAKING**: EF Core migration files reference old namespaces and must be regenerated
- No runtime behavior changes, no database changes, no user-facing impact
