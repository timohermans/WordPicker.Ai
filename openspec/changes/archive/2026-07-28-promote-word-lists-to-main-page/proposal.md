## Why

The home page at `/` shows a generic "Welcome" page with no functional value. Users immediately navigate to `/WordLists/Index` to get started. Making the word list overview the root page eliminates a useless redirect and puts users straight into the app. Separately, the footer floats up on short pages instead of sticking to the bottom.

## What Changes

- Root `Index.cshtml` redirects to `/WordLists/Index` — the word list overview remains the functional entry point with no code duplication
- Update the navbar "Home" link to point to `/WordLists/Index` (removes "Home" concept, keeps just "Word Lists")
- Fix the footer to always stay at the bottom using Bootstrap utility classes

## Capabilities

### New Capabilities

None — no new functional capabilities are introduced.

### Modified Capabilities

None — the route change is an implementation detail. The requirements for viewing, creating, and deleting word lists remain the same.

## Impact

- `src/WordPicker.Web/Pages/Index.cshtml` — changed to redirect to `/WordLists/Index`
- `src/WordPicker.Web/Pages/Index.cshtml.cs` — changed to return `RedirectToPage("/WordLists/Index")`
- `src/WordPicker.Web/Pages/Shared/_Layout.cshtml` — nav link + footer classes
- `src/WordPicker.Web/wwwroot/css/site.css` — remove legacy sticky footer CSS
