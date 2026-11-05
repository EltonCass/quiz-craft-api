# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

QuizCraft / "Brain Bites": a .NET 10 ASP.NET Core API for quizzes, categories and questions (multiple-option and fill-in-blank). It started as the backend for an SPA. The direction is now to add an **MCP server** (and later MCP Apps UI) *alongside* the existing REST API, which stays. See "MCP direction" below.

## Commands

```bash
dotnet restore
dotnet build
dotnet test                                                    # whole solution
dotnet test tests/QuizCraft.Application.Tests                  # one project
dotnet test --filter "FullyQualifiedName~CategoryHandlerTests" # one class/test by name
dotnet run --project src/API/QuizCraft.Api                     # Swagger at /swagger, health at /health
docker compose up -d db                                        # local Postgres on :5432
```

- Target framework is set once in `Directory.Build.props` (`NetCoreFramework`); every project uses `$(NetCoreFramework)`. Package versions are centrally managed in `Directory.Packages.props` (no versions in `.csproj` files).
- `QuizCraft.Integration.Tests` uses Testcontainers (needs Docker) but its tests are currently reported as skipped.
- StyleCop and SonarAnalyzer run at build time; `.editorconfig` holds the rules. Source files start with a copyright header comment (`// Copyright (c) ... Elton Cassas` / `// See LICENSE.txt`).

## Architecture

Clean-architecture layout under `src/`, enforced by `tests/QuizCraft.Architecture.Tests` (NetArchTest). Run those tests after adding a project or changing references.

- `Core/QuizCraft.Models`: entities, DTOs (`*ForDisplay`, `*ForUpsert`, question DTOs), `RequestError`, settings classes.
- `Core/QuizCraft.Application`: `I*Handler` services (business logic), `I*Repository` interfaces, FluentValidation validators, Mapster mappings (`QuizMappingConfig`). Wired in `ApplicationServiceRegistration`.
- `Infrastructure/*`: repository implementations and EF Core configurations, email (SendGrid).
- `API/QuizCraft.Api`: controllers, `Program.cs`, middleware, OpenAI-based quiz suggestion.

Things that need several files to understand:

- **Results use `OneOf<T, RequestError>`**, not exceptions. Handlers return it; controllers check `result.IsT0` and call the `HandleError` extension (`Helpers/ErrorRequestHelper.cs`), which maps `RequestError.StatusCode` to an HTTP result. Any new front end (including MCP tools) should consume the handlers and map `RequestError` itself.
- **Controllers are thin**; logic lives in `I*Handler`. Reuse the handlers rather than the controllers or repositories.
- **Questions are polymorphic**: `QuestionHandler` plus `ISpecificQuestionHandler<TDto>` implementations (`MultipleOptionQuestionHandler`, `FillInBlankQuestionHandler`), each with its own validator and repository.
- **Two persistence projects exist**: `QuizCraft.Persistence.Postgresql` is the active one (registered in `Program.cs`, used by the integration test base). `QuizCraft.Persistence` (SQL Server, older migrations) is still referenced but its registration is commented out in `Program.cs`. Make schema changes in the Postgresql project.
- **API versioning**: routes are `api/v{version:apiVersion}/[controller]`, default 1.0.
- **Logging**: Serilog (console, file, Application Insights), configured in `Program.cs` and `appsettings.json`.

## Configuration

`appsettings.json` holds placeholders (`ConnectionStrings:QuizAPIConnectionString`, `ExternalServices:OpenAPIKey`, `ApplicationInsights`, `EmailSettings`). Real values come from user secrets or environment variables (e.g. `ConnectionStrings__QuizAPIConnectionString`, as in `docker-compose.yml`). `QuizGeneration` reads `OpenAPIKey` from configuration.

## MCP direction

Done: the server is mounted at `/mcp` (stateless Streamable HTTP, registered in `Program.cs`). Tools live in `src/API/QuizCraft.Api/Mcp/QuizTools.cs` and call the `I*Handler` services; `RequestError` is surfaced as an `McpException`. Currently read-only: `list_categories`, `list_quizzes`, `get_quiz`. Locally, `appsettings.json` placeholders break startup (e.g. the Application Insights connection string); override via environment variables.

Agreed plan, in order: (1) platform upgrade to .NET 10 (done); (2) MCP server with read-only tools (done), then write tools; (3) stop leaking `CorrectAnswer` to quiz takers (today `QuestionForDisplay` always includes it) and add a separate check-answer tool; (4) add OAuth: the app has no authentication today (`AuthenticationSettings` is empty, the Swagger security filter is commented out, `AllowedHosts` is `*`). The MCP endpoint must act as an OAuth resource server (RFC 9728 protected-resource metadata, `401` with `WWW-Authenticate`, audience-validated tokens); (5) MCP Apps quiz UI served as a `ui://` resource with MIME type `text/html;profile=mcp-app`, using the Begonia design system. Verify the `ModelContextProtocol.Extensions.Apps` package API before relying on it.

Known leftovers: the `OpenAI` package is the old `OpenAI_API` library (v1.7.2); `QuizPromptSuggestionsController` returns a placeholder option (`"O1"`). With MCP, Claude can write the questions itself, so this path may be replaced.
