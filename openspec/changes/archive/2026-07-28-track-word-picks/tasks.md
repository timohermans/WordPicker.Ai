## 1. Data model changes

- [x] 1.1 Create `PickHistory` model with `Id`, `WordEntryId`, `WordListId`, `PickedAt` properties
- [x] 1.2 Add `CurrentPickCycle` (int, default 0) to `WordList`
- [x] 1.3 Add `LastPickCycle` (int, default 0) to `WordEntry`
- [x] 1.4 Add `PickHistory` navigation collection to `WordList` and `WordEntry`
- [x] 1.5 Add `PickHistory` DbSet and fluent config in `ApplicationDbContext` (FKs with cascade delete, indexes)
- [x] 1.6 Update `WordEntry` and `WordList` config in `ApplicationDbContext` for new properties

## 2. Migration

- [x] 2.1 Create EF Core migration for new PickHistory table and new columns

## 3. Pick page algorithm update

- [x] 3.1 Update `OnPostAsync` in `Pick.cshtml.cs` to use cycle-based selection: query unpicked entries (`LastPickCycle < CurrentPickCycle`), advance cycle if none found, pick randomly, record in PickHistory
- [x] 3.2 Include picked-word count from cycle in the response (for the result display, but user-facing behavior stays the same)

## 4. Details page history display

- [x] 4.1 Add `PickHistory` query in `DetailsModel.OnGetAsync` to load last 20 picks (ordered by `PickedAt` desc)
- [x] 4.2 Add `PickHistory` display list to `Details.cshtml`

## 5. Write tests

- [x] 5.1 Test PickHistory model creation and relationships
- [x] 5.2 Test cycle-based pick algorithm: picks exhaust all words before repeating
- [x] 5.3 Test cycle reset when all words are picked
- [x] 5.4 Test new words added mid-cycle are immediately available
- [x] 5.5 Test cascade delete: removing a word deletes its PickHistory entries
