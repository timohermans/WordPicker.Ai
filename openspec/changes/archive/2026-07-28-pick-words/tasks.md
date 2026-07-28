## 1. Add "Pick words" button to Index page

- [x] 1.1 Add a "Pick words" link/button to `Index.cshtml` that navigates to `/WordLists/Pick`

## 2. Create Pick page (single page with selection + results)

- [x] 2.1 Create `Pick.cshtml` with a form containing checkboxes for each word list, a submit button, and a back link
- [x] 2.2 Create `Pick.cshtml.cs` with `OnGetAsync` that loads all word lists
- [x] 2.3 Add a results section in `Pick.cshtml` (hidden on GET, shown on POST) that displays each picked word with its source list name
- [x] 2.4 Implement `OnPostAsync` that picks one random word per selected list via `OrderBy(_ => Guid.NewGuid()).FirstOrDefault()`
- [x] 2.5 Add validation to reject empty selection with a validation message
- [x] 2.6 Handle empty state when no word lists exist
- [x] 2.7 Handle lists with no words gracefully ("No words in this list")
- [x] 2.8 Add "Pick again" button that resets to the selection view (GET request)

## 3. Write tests

- [x] 3.1 Test Pick page: GET loads all lists
- [x] 3.2 Test Pick page: POST with no selection shows validation message
- [x] 3.3 Test Pick page: POST with valid selection with no words in list shows placeholder
- [x] 3.4 Test Pick page: POST with valid selection shows results inline
