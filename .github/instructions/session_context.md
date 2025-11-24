---
applyTo: '**'
lastUpdated: 2025-11-24T12:00:00Z
sessionStatus: active
---

# Current Session Context

## Active Task
Fix GitHub Pages deployment issues (resource 404s and SRI failure).

## Todo List Status
- [x] Step 1: Add Google Fonts to `wwwroot/index.html`
- [x] Step 2: Update `wwwroot/css/app.css` with new color palette and styles
- [x] Step 3: Update `Pages/Index.razor` structure for better styling
- [x] Step 4: Update `Shared/NavMenu.razor` styling
- [x] Step 5: Verify changes
- [x] Step 6: Add skill proficiency meters to `Pages/Skills.razor` and CSS
- [x] Step 7: Final verification and accessibility tweaks
- [x] Step 8: Read session context (2025-11-22)
- [x] Step 9: Patch `wwwroot/index.html` (set base href and update SRI) (2025-11-22)
- [ ] Step 10: Verify resources load on GitHub Pages (or local preview)
- [x] Step 11: If needed: remove or update SRI for external CDN or make assets relative (2025-11-24)
- [ ] Step 12: Update session_context.md with final state and mark session complete

## Key Technical Decisions
- Primary Color: #722f37 (Merlot)
- Secondary Color: #D4AF37 (Gold) for accents
- Background: Dark theme (#0f172a)
- Fonts: Montserrat (Headings), Open Sans (Body)

## Recent File Changes
 - `wwwroot/index.html`: Set `<base href="/Portfolio/" />` for GitHub Pages, updated the `github-markdown-css` SRI to match the computed SHA-512 value reported by the browser console, then removed the `integrity` & `crossorigin` attributes to prevent brittle SRI mismatches when deployed on GitHub Pages (2025-11-24).

## Next Session Priority
- Verify the site on GitHub Pages (or locally via `dotnet publish` and a static server) to ensure all resources load correctly; if any SRI/404 issues remain, remove or update the SRI attribute as a fallback.

## Session Notes
Completed the styling overhaul earlier. On 2025-11-22 I investigated GitHub Pages deployment errors and patched `wwwroot/index.html` to set the repository base path and update the `github-markdown-css` integrity attribute.
