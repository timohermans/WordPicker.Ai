## 1. Add "Pick words" button to Index page

- [ ] 1.1 Add a "Pick words" link/button to `Index.cshtml` that navigates to `/WordLists/Pick`

## 2. Create Pick page (single page with selection + results)

- [ ] 2.1 Create `Pick.cshtml` with a form containing checkboxes for each word list, a submit button, and a back link
- [ ] 2.2 Create `Pick.cshtml.cs` with `OnGetAsync` that loads all word lists
- [ ] 2.3 Add a results section in `Pick.cshtml` (hidden on GET, shown on POST) that displays each picked word with its source list name
- [ ] 2.4 Implement `OnPostAsync` that picks one random word per selected list via `OrderBy(_ => Guid.NewGuid()).FirstOrDefault()`
- [ ] 2.5 Add validation to reject empty selection with a validation message
- [ ] 2.6 Handle empty state when no word lists exist
- [ ] 2.7 Handle lists with no words gracefully ("No words in this list")
- [ ] 2.8 Add "Pick again" button that resets to the selection view (GET request)

## 3. Write tests

- [ ] 3.1 Test Pick page: GET loads all lists
- [ ] 3.2 Test Pick page: POST with no selection shows validation message
- [ ] 3.3 Test Pick page: POST with valid selection with no words in list shows validation message
- [ ] 3.4 Test Pick page: POST with valid selection shows results inline
