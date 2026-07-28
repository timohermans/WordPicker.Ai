## Context

WordPicker is a Razor Pages app with a standard scaffolded home page that serves no purpose. The functional entry point is the word list overview at `/WordLists/Index`. The layout uses the classic ASP.NET Core sticky footer pattern (margin-bottom on body, absolute positioning) but the footer CSS rule was never added, so the footer floats up on short pages.

## Goals / Non-Goals

**Goals:**
- Make `/` redirect to the word list overview instead of showing a blank welcome page
- Keep `WordLists/Index` untouched — no code duplication, no moved files
- Fix the footer to stick to the bottom of the viewport regardless of content height

**Non-Goals:**
- No functional changes to word list management or word entry management
- No file deletion or file movement
- No spec changes required

## Decisions

1. **Root Index redirects to WordLists/Index**
   - Rationale: Simplest possible change. `Index.cshtml.cs` returns `RedirectToPage("/WordLists/Index")`. No files moved, no routes broken, no tests need updating.
   - Alternative considered: Move content to root (rejected — user wants to keep WordLists/Index in place).

2. **Bootstrap utility classes for sticky footer**
   - `d-flex flex-column min-vh-100` on `<body>`, `d-flex flex-column flex-grow-1` on `.container` (the flex direct child of body), `d-flex align-items-center` on footer
   - `flex-grow-1` must be on the direct child of the flex container (`.container` div, not `<main>` inside it)
   - Footer gets `min-height: 60px` via `.footer` class in `site.css` (Bootstrap has no height utility for 60px)
   - Rationale: Native Bootstrap 5 utilities for the layout, one small CSS rule for the footer height.

## Risks / Trade-offs

- **Extra redirect** — Visiting `/` adds a 302 redirect before reaching the content. Negligible for this app's scale.
- **Nav link label** — The navbar currently has "Home" and "Word Lists" pointing to the same place after the change. Cleanest fix is to just keep "Word Lists" and remove "Home".
