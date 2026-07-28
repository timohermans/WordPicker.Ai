## Context

Currently all source code, project files, and configuration sit at repo root alongside build output (`bin/`, `obj/`), the test project, and project metadata (`openspec/`, `.gitignore`). The single `.csproj` is named `WordPicker.csproj` with root namespace `WordPicker`.

## Goals / Non-Goals

**Goals:**
- Standard `src/` + `tests/` layout with `.sln` at root
- Web project at `src/WordPicker.Web/` with namespace `WordPicker.Web`
- Test project at `tests/WordPicker.Tests/` with namespace `WordPicker.Tests`
- Regenerate EF Core migrations to absorb namespace changes
- Everything compiles and tests pass after the move

**Non-Goals:**
- No behavioral changes, no new features
- No spec changes (requirements stay identical)
- No CI/CD setup (future concern)

## Decisions

1. **Migrations: delete + regenerate (Option B)** — Migration files contain hardcoded namespace strings (`WordPicker.Data`, `WordPicker.Models.WordEntry`). Manually patching them is error-prone and tedious. Since there's no production database, regeneration is the cleanest path.

2. **Solution file contents** — Single `.sln` with two projects: `src/WordPicker.Web/WordPicker.Web.csproj` and `tests/WordPicker.Tests/WordPicker.Tests.csproj`. No solution folders needed yet.

3. **Namespace rename scope** — All `.cs` and `.cshtml` files in the web project change from `WordPicker.*` to `WordPicker.Web.*`. Test project keeps namespace `WordPicker.Tests`.

4. **`openspec/` stays at root** — It describes the entire project (including future infra), not just the web app.

## Risks / Trade-offs

- [Risk] Missing a namespace reference in a `.cshtml` or `.cs` file → Mitigation: build will catch all compile errors; `dotnet build` after move is the verification step
- [Risk] EF migration regeneration could produce different model snapshot if entity config changed → Mitigation: verify the generated migration matches the model. The entity config hasn't changed, so the new migration should be identical in effect
- [Trade-off] Deleting old migrations loses git history for those files → Acceptable since the initial migration has no production data and git retains history
