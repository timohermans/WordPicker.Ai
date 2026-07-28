## Why

The current word picker repeats words indefinitely with no tracking. A teacher who picks words from a vocabulary list for weekly quizzes needs each word picked exactly once before any repeats — otherwise some words are neglected. There's also no way to see which words were picked and when.

## What Changes

- New `PickHistory` table to record every word pick with a timestamp
- Add `CurrentPickCycle` to `WordList` and `LastPickCycle` to `WordEntry` for the cycle-reset algorithm
- Pick page now uses cycle-based selection: only unpicked words (this cycle) are eligible; when exhausted, the cycle advances and all words become eligible again
- Word list detail page shows the last 20 picks with timestamps
- Cascade delete PickHistory when a WordEntry is deleted

## Capabilities

### New Capabilities

- *(none — the tracking is part of existing capabilities)*

### Modified Capabilities
- `pick-words`: Picking now uses a cycle algorithm that exhausts all words before repeating; each pick is recorded
- `word-entry-management`: Removing a word cascades delete to its pick history

## Impact

- New model: `PickHistory` (`PickHistory.cs`)
- Model changes: `WordList.CurrentPickCycle` (int), `WordEntry.LastPickCycle` (int)
- New EF Core migration
- Modified: `Pick.cshtml.cs` (new pick algorithm), `Details.cshtml`/`.cshtml.cs` (pick history display)
- `ApplicationDbContext.OnModelCreating` — new PickHistory config + relationship updates
- No new pages, no new dependencies
