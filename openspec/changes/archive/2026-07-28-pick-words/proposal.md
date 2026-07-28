## Why

Word list owners have no way to randomly pick words from their lists. A teacher who maintains vocabulary lists per lesson wants to quickly pull one random word per list for a quiz or review session — currently they'd need to manually scan each list.

## What Changes

- Add a "Pick words" button to the word lists overview page (`/WordLists/Index`)
- New "Pick Words" page (`/WordLists/Pick`) with a multi-select UI to choose which lists to draw from, followed by inline results showing one randomly selected word per chosen list with the source list name
- The new page follows the existing conventions (Razor Pages, EF Core directly in page models, Bootstrap 5, no new projects)

## Capabilities

### New Capabilities
- `pick-words`: Random word selection from one or more word lists, with results display

### Modified Capabilities

- *(none — no existing spec requirements are changing)*

## Impact

- New Razor Page: `Pick.cshtml`/`Pick.cshtml.cs` under `Pages/WordLists/`
- Minor addition to `Pages/WordLists/Index.cshtml` for the button
- No new models, no new database tables, no new dependencies
- No breaking changes
