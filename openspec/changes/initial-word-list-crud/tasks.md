## 1. Project Setup

- [ ] 1.1 Add .gitignore for ASP.NET projects
- [ ] 1.2 Create new ASP.NET Core Razor Pages project in the repository root
- [ ] 1.3 Add NuGet packages: EF Core, Npgsql provider, EF Core Design tools
- [ ] 1.4 Add Bootstrap 5 and Alpine.js to the project (CDN)
- [ ] 1.5 Create appsettings.json with empty config placeholders (connection strings, etc.)
- [ ] 1.6 Create appsettings.Development.json with actual development config values
- [ ] 1.7 Add appsettings.Development.json to .gitignore

## 2. Data Layer

- [ ] 2.1 Create WordList entity model (Id, Name, CreatedAt, UpdatedAt)
- [ ] 2.2 Create WordEntry entity model (Id, WordListId, Value, CreatedAt)
- [ ] 2.3 Create ApplicationDbContext with DbSets for both entities
- [ ] 2.4 Configure entity relationships and constraints in OnModelCreating
- [ ] 2.5 Generate initial EF Core migration
- [ ] 2.6 Apply migration to PostgreSQL database

## 3. Test Project Setup

- [ ] 3.1 Create MSTest project (WordPicker.Tests) referencing main project
- [ ] 3.2 Add NuGet packages: Microsoft.EntityFrameworkCore.InMemory, MSTest v4
- [ ] 3.3 Create test helper to generate in-memory SQLite DbContext
- [ ] 3.4 Add MSTest test categories attribute conventions

## 4. Word List Management

- [ ] 4.1 Create WordLists page model with OnGet, OnPost, OnGetDelete handlers
- [ ] 4.2 Implement OnGet to list all word lists sorted by recent
- [ ] 4.3 Implement OnPost for creating word lists with validation
- [ ] 4.4 Implement Details page to show list with its words
- [ ] 4.5 Implement Delete page with confirmation

## 5. Word List Management Tests

- [ ] 5.1 Write tests for index page returning all lists sorted by recent
- [ ] 5.2 Write tests for create page with valid/invalid data
- [ ] 5.3 Write tests for details page with existing/non-existing list
- [ ] 5.4 Write tests for delete page with confirmation

## 6. Word List Pages

- [ ] 6.1 Create Index page displaying list of word lists
- [ ] 6.2 Create shared _Layout with Bootstrap navbar
- [ ] 6.3 Create Create form page with validation
- [ ] 6.4 Create Details page showing list name and words
- [ ] 6.5 Add delete confirmation using Alpine.js

## 7. Word Entry Management

- [ ] 7.1 Create WordEntries page model with Add and Remove handlers
- [ ] 7.2 Implement OnPostAdd with duplicate word validation
- [ ] 7.3 Implement OnPostRemove with confirmation
- [ ] 7.4 Create partial view for adding new words to a list
- [ ] 7.5 Add word removal UI with Alpine.js confirmation

## 8. Word Entry Management Tests

- [ ] 8.1 Write tests for add action with valid/invalid/duplicate words
- [ ] 8.2 Write tests for remove action with confirmation

## 9. Integration & Polish

- [ ] 9.1 Wire up word add/remove in the list Details page
- [ ] 9.2 Add client-side validation for forms
- [ ] 9.3 Style all pages consistently with Bootstrap
- [ ] 9.4 Add empty state messages when no lists or words exist
