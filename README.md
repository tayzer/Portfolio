# Taylor Wilcox — Portfolio

Blazor WebAssembly portfolio, targeting .NET 8. `global.json` selects the .NET 8 SDK to match GitHub Pages builds.

## Live site

**https://tayzer.github.io/Portfolio/**

Verified on 18 November 2026: GitHub Pages reports this URL with no custom domain configured, and the site returns HTTP 200. The root `CNAME` contains the historical address `me.bitswirl.co.uk`, which no longer resolves.

Deployment workflows are in `.github/workflows/`. `deploy-pages.yml` publishes through the GitHub Pages workflow on pushes to `master`; `deploy.yml` targets `main` and the `gh-pages` branch. Check the repository's Pages settings before changing deployment configuration.

## Local development

```powershell
dotnet restore MyPortfolio.csproj -r browser-wasm
dotnet run --project MyPortfolio.csproj --no-launch-profile --urls http://localhost:60665
```

## Theme and content

The printed-paper theme in `wwwroot/css/field-notes.css` draws on the visual direction of [abateslop.com](https://abateslop.com/): cream paper, condensed headings, strong rules, red accents and warm gold highlights (`#e6bd50`). Portfolio branding, copy, imagery and navigation remain specific to Taylor.

The theme defaults to light. The Theme button switches between paper and dark variants and remembers the choice when browser storage is available. Shared component styles remain in `wwwroot/css/app.css`; the theme stylesheet loads afterward.

Home, contact, project and skill content live in `wwwroot/data/`. The knowledge base loads from `tayzer/KnowledgeBase.Dev` on GitHub.

## Theme verification — 18 November 2026

- Release build and production publish succeeded with .NET SDK 8.0.415.
- Browser checks covered home, project list/detail, skills and knowledge-base list/Markdown rendering.
- Checked desktop and 390px mobile layouts; no horizontal page overflow observed on the checked mobile pages.
- Dark theme survived reload; switching back restored the paper theme.
- The theme refresh has not been deployed.
