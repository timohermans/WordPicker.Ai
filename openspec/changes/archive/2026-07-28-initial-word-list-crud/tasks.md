## 1. Project Setup

- [x] 1.1 Add .gitignore for ASP.NET projects
- [x] 1.2 Create new ASP.NET Core Razor Pages project in the repository root
- [x] 1.3 Add NuGet packages: EF Core, Npgsql provider, EF Core Design tools
- [x] 1.4 Add Bootstrap 5 and Alpine.js to the project (CDN)
- [x] 1.5 Create appsettings.json with empty config placeholders (connection strings, etc.)
- [x] 1.6 Create appsettings.Development.json with actual development config values
- [x] 1.7 Add appsettings.Development.json to .gitignore

## 2. Data Layer

- [x] 2.1 Create WordList entity model (Id, Name, CreatedAt, UpdatedAt)
- [x] 2.2 Create WordEntry entity model (Id, WordListId, Value, CreatedAt)
- [x] 2.3 Create ApplicationDbContext with DbSets for both entities
- [x] 2.4 Configure entity relationships and constraints in OnModelCreating
- [x] 2.5 Generate initial EF Core migration
- [x] 2.6 Apply migration to PostgreSQL database

## 3. Test Project Setup

- [x] 3.1 Create MSTest project (WordPicker.Tests) referencing main project
- [x] 3.2 Add NuGet packages: Microsoft.EntityFrameworkCore.InMemory, MSTest v4
- [x] 3.3 Create test helper to generate in-memory SQLite DbContext
- [x] 3.4 Add MSTest test categories attribute conventions

## 4. Word List Management

- [x] 4.1 Create WordLists page model with OnGet, OnPost, OnGetDelete handlers
- [x] 4.2 Implement OnGet to list all word lists sorted by recent
- [x] 4.3 Implement OnPost for creating word lists with validation
- [x] 4.4 Implement Details page to show list with its words
- [x] 4.5 Implement Delete page with confirmation

## 5. Word List Management Tests

- [x] 5.1 Write tests for index page returning all lists sorted by recent
- [x] 5.2 Write tests for create page with valid/invalid data
- [x] 5.3 Write tests for details page with existing/non-existing list
- [x] 5.4 Write tests for delete page with confirmation

## 6. Word List Pages

- [x] 6.1 Create Index page displaying list of word lists
- [x] 6.2 Create shared _Layout with Bootstrap navbar
- [x] 6.3 Create Create form page with validation
- [x] 6.4 Create Details page showing list name and words
- [x] 6.5 Add delete confirmation using Alpine.js

## 7. Word Entry Management

- [x] 7.1 Create WordEntries page model with Add and Remove handlers
- [x] 7.2 Implement OnPostAdd with duplicate word validation
- [x] 7.3 Implement OnPostRemove with confirmation
- [x] 7.4 Create partial view for adding new words to a list
- [x] 7.5 Add word removal UI with Alpine.js confirmation

## 8. Word Entry Management Tests

- [x] 8.1 Write tests for add action with valid/invalid/duplicate words
- [x] 8.2 Write tests for remove action with confirmation

## 9. Integration & Polish

- [x] 9.1 Wire up word add/remove in the list Details page
- [x] 9.2 Add client-side validation for forms
- [x] 9.3 Style all pages consistently with Bootstrap
- [x] 9.4 Add empty state messages when no lists or words exist
