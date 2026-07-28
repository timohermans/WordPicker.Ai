## Context

The existing WordPicker app has word lists with word entries. Users can create lists, add/remove words, and delete lists. There is currently no functionality to randomly select words. The user wants to pick one random word from each of several selected lists and see the results.

All page models use EF Core `DbContext` directly (no Repository pattern). The frontend uses Bootstrap 5 with Alpine.js for interactivity.

## Goals / Non-Goals

**Goals:**
- Add a "Pick words" button to the word lists overview page
- Provide a page where users can select multiple lists via checkboxes
- Pick one random word per selected list and display the results
- Show the source list name alongside each picked word

**Non-Goals:**
- Weighted or configurable word selection
- Picking multiple words per list
- Persisting pick results (results are ephemeral per request)
- Any changes to existing data models or database schema

## Decisions

1. **Single Razor Page (`Pick`) with two display states**
   - GET: shows checkboxes for list selection with a submit button
   - POST: picks one random word per selected list and returns the same page with results displayed inline below the form
   - A "Pick again" button resets back to the selection state
   - No redirect, no query string, no TempData — the results are computed in the POST handler and passed through the page model

2. **Random selection done server-side via `OrderBy(_ => Guid.NewGuid()).FirstOrDefault()`**
   - Simple, well-known EF Core pattern for random row selection
   - No need for database-side `RANDOM()` or `NEWID()` extensions
   - Guid-based randomization is sufficient for this use case; cryptographic randomness is not needed

3. **Button placed in the Index page toolbar area**
   - A "Pick words" button at the top of the word lists index, styled as a primary button
   - Consistent with the existing UI patterns

## Risks / Trade-offs

- [List has no words] → If a selected list is empty, no word can be picked. Mitigation: the results section will indicate "No words in this list" for empty lists rather than silently omitting them.
