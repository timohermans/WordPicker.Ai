## 1. Update root Index to redirect

- [x] 1.1 Change `Index.cshtml.cs` to redirect: replace `OnGet()` body with `return RedirectToPage("/WordLists/Index");` and change return type to `IActionResult`
- [x] 1.2 Update `Index.cshtml` to just show a redirect message or leave minimal shell (can be a simple page with no visible content since it redirects immediately)
- [x] 1.3 Create `tests/WordPicker.Tests/Web/Pages/HomePageRedirectTests.cs` with a unit test that instantiates `IndexModel`, calls `OnGet()`, and asserts the result is a `RedirectToPageResult` with `PageName` equal to `/WordLists/Index`

## 2. Update layout

- [x] 2.1 In `_Layout.cshtml`, remove the "Home" nav item (since it would point to the same place as "Word Lists")
- [x] 2.2 Add `d-flex flex-column min-vh-100` to `<body>`, `d-flex flex-column flex-grow-1` to the `.container` div (direct child of body), `d-flex align-items-center` to `<footer>`
- [x] 2.3 Drop `flex-grow-1` from `<main>` — it's inside `.container`, not a direct flex child

## 3. Clean up CSS

- [x] 3.1 In `site.css`, remove the old sticky footer rules (`html { position: relative; min-height: 100%; }` and `body { margin-bottom: 60px; }`)
- [x] 3.2 Add `.footer { min-height: 60px; }` to `site.css`

## 4. Verify

- [x] 4.1 Run `dotnet build` to ensure no compilation errors
- [x] 4.2 Run tests to confirm all pass
