---
applyTo: '**'
lastUpdated: 2025-11-25T11:00:00Z
sessionStatus: complete
---

# Current Session Context

## Active Task
Implement dynamic Guides page that pulls content from GitHub repo (KnowledgeBase.Dev).

## Todo List Status
- [x] Step 1: Fetch and analyze KnowledgeBase.Dev repo structure
- [x] Step 2: Research GitHub API for fetching repo contents  
- [x] Step 3: Create GitHubKnowledgeBaseService to fetch repo contents
- [x] Step 4: Create data models for GitHub content items
- [x] Step 5: Update Guides.razor to display folders/files dynamically
- [x] Step 6: Add navigation for nested folders (breadcrumbs)
- [x] Step 7: Add styling for the guides page
- [x] Step 8: Test the implementation
- [x] Step 9: Update session_context.md with final state

## Key Technical Decisions
- Using GitHub REST API (api.github.com) to fetch repo contents dynamically
- Using raw.githubusercontent.com for fetching raw markdown file contents  
- Implemented client-side caching (10 minutes) to reduce API calls
- GitHub API supports CORS for public repos - no server proxy needed
- Catch-all route parameter `{*FilePath}` for nested folder/file navigation

## Recent File Changes
- `Services/GitHubKnowledgeBaseService.cs`: New service to fetch GitHub repo contents and markdown files
- `Program.cs`: Registered GitHubKnowledgeBaseService as a scoped service
- `Pages/Guides.razor`: Complete rewrite to dynamically display folders/files from GitHub repo
- `wwwroot/css/app.css`: Added comprehensive styling for guides page (breadcrumbs, content grid, markdown rendering)
- `Shared/NavMenu.razor`: Updated nav link text from "Guides" to "Knowledge Base"

## External Resources Referenced
- GitHub REST API Contents endpoint: https://docs.github.com/en/rest/repos/contents
- KnowledgeBase.Dev repo: https://github.com/tayzer/KnowledgeBase.Dev

## Next Session Priority
No active tasks - feature complete. Consider testing on GitHub Pages after deployment.

## Session Notes
Successfully implemented dynamic GitHub Knowledge Base integration:
- Fetches folder structure from tayzer/KnowledgeBase.Dev repo via GitHub API
- Renders markdown files using Markdig
- Supports nested folder navigation with breadcrumb trail
- Shows folders first, then markdown files alphabetically
- Includes "View on GitHub" links for source files
- 10-minute client-side caching to respect API rate limits
