## 1. Create directory structure and move web project

- [x] 1.1 Create `src/WordPicker.Web/` and `tests/` directories
- [x] 1.2 Move and rename web project: `WordPicker.csproj` → `src/WordPicker.Web/WordPicker.Web.csproj`
- [x] 1.3 Move `Program.cs`, `appsettings.json`, `appsettings.Development.json` into `src/WordPicker.Web/`
- [x] 1.4 Move `Data/`, `Models/`, `Pages/`, `Migrations/`, `Properties/`, `wwwroot/` into `src/WordPicker.Web/`

## 2. Rename namespace from WordPicker to WordPicker.Web

- [x] 2.1 Update `namespace WordPicker.*` to `namespace WordPicker.Web.*` in all `.cs` files under `src/WordPicker.Web/`
- [x] 2.2 Update `using WordPicker.*` to `using WordPicker.Web.*` in all `.cs` files under `src/WordPicker.Web/`
- [x] 2.3 Update `@using WordPicker` → `@using WordPicker.Web` and `@namespace WordPicker.Pages` → `@namespace WordPicker.Web.Pages` in `_ViewImports.cshtml`
- [x] 2.4 Update `@model WordPicker.Pages.*` references in `.cshtml` files
- [x] 2.5 Update `WordPicker.Models.WordEntry` string references in migration designer files (or prepare for regeneration)

## 3. Move test project

- [x] 3.1 Move `WordPicker.Tests/` directory into `tests/WordPicker.Tests/`
- [x] 3.2 Update project reference path in `tests/WordPicker.Tests/WordPicker.Tests.csproj` from `..\WordPicker.csproj` to `..\..\src\WordPicker.Web\WordPicker.Web.csproj`
- [x] 3.3 Update `using WordPicker.*` to `using WordPicker.Web.*` in all test `.cs` files

## 4. Create solution file

- [x] 4.1 Generate `WordPicker.sln` with `dotnet new sln`
- [x] 4.2 Add both projects to solution: `src/WordPicker.Web/WordPicker.Web.csproj` and `tests/WordPicker.Tests/WordPicker.Tests.csproj`

## 5. Regenerate EF Core migrations

- [x] 5.1 Delete existing migration files from `src/WordPicker.Web/Migrations/`
- [x] 5.2 Run `dotnet ef migrations add InitialCreate` from `src/WordPicker.Web/` directory
- [x] 5.3 Verify generated migration builds correctly

## 6. Clean up and verify

- [x] 6.1 Delete stale `bin/` and `obj/` directories from repo root
- [x] 6.2 Run `dotnet build` on the solution to verify everything compiles
- [x] 6.3 Run `dotnet test` to verify all tests pass
