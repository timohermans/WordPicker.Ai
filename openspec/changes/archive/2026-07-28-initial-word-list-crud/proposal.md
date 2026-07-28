## Why

The core functionality of WordPicker revolves around organizing and manipulating word lists. Currently, there is no system in place to create, manage, or store these lists. Establishing a robust word list management capability is the foundational step for all future features.

## What Changes

- New ASP.NET Core Razor Pages web application with Bootstrap and Alpine.js
- Ability to create new word lists with a name
- Ability to view all existing word lists
- Ability to view words within a specific list
- Ability to add words to a list
- Ability to remove words from a list
- Ability to delete an entire word list
- Entity Framework Core integration with PostgreSQL for data persistence

## Capabilities

### New Capabilities

- `word-list-management`: CRUD operations for word lists (create, read, delete lists)
- `word-entry-management`: CRUD operations for adding/removing individual words within a list

### Modified Capabilities

<!-- None - this is the initial setup -->

## Impact

- **Code**: New ASP.NET Core Razor Pages project structure
- **Database**: New PostgreSQL database with Entity Framework Core migrations
- **Dependencies**: Addition of EF Core, Npgsql provider, Bootstrap, Alpine.js
- **UI**: New responsive web interface for list management
