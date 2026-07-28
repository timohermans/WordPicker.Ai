## Context

The current pick feature selects a random word via `OrderBy(_ => Random.Shared.Next()).FirstOrDefault()` with no tracking — the same word can be picked repeatedly. The user wants a cycle-based system where all words must be picked before any repeats. Every pick is tracked in a history table, and the last 20 picks are visible on the word list detail page.

## Goals / Non-Goals

**Goals:**
- Cycle-based pick algorithm that exhausts all words before repeating
- New `PickHistory` table recording every pick with timestamp
- Show last 20 picks on the word list detail page
- Cascade delete history when a word is removed
- New words added mid-cycle are immediately available for picking

**Non-Goals:**
- Per-user tracking (no auth)
- Weighted or configurable selection
- UI indicators of remaining words per cycle

## Decisions

1. **Cycle tracking via integer fields instead of a separate reset table**
   - `WordList.CurrentPickCycle` (int, default 0) — incremented each time all words in a list are exhausted
   - `WordEntry.LastPickCycle` (int, default 0) — the cycle in which this word was last picked
   - Available = entries where `LastPickCycle < CurrentPickCycle`; when none found, increment `CurrentPickCycle` (resets all)
   - This avoids needing a reset event table and is constant-time to check

2. **PickHistory as a separate table with WordListId denormalized**
   - Columns: `Id`, `WordEntryId`, `WordListId`, `PickedAt`
   - `WordListId` avoids a join through WordEntry when querying "last 20 picks for this list"
   - FK from `WordEntryId` with cascade delete
   - FK from `WordListId` with cascade delete

3. **History displayed on existing Details page**
   - A new "Pick History" section at the bottom of `Details.cshtml`
   - Queried separately from the word list query to keep each query focused
   - Last 20 ordered by `PickedAt` descending

4. **Pick algorithm runs entirely server-side in PickModel.OnPostAsync**
   - Same pattern as before: no new page, same page with inline results
   - Algorithm: load eligible entries → if none, advance cycle → pick randomly → record history → display

## Risks / Trade-offs

- [Cycle integer overflow] → At 100 picks/day, ~50,000 years. Not a practical concern.
- [New word has LastPickCycle=0, CurrentPickCycle>0] → New words are immediately available, which is desired behavior. If this isn't wanted, new words could start at `CurrentCycle`, but the user confirmed they should be available immediately.
- [History table growth] → For a personal vocabulary app, history growth is negligible. No archiving needed.
