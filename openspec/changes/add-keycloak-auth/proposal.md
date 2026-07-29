## Why

The app currently has no authentication — all pages are public. Adding Keycloak-backed OpenID Connect authentication secures the application and lays the groundwork for per-user word lists in the future.

## What Changes

- Add `Microsoft.AspNetCore.Authentication.OpenIdConnect` package
- Configure OIDC authentication with Keycloak (realm: `home`, authority: `https://<redacted>.nl`)
- Require authentication on all Razor Pages globally
- Store Keycloak client ID and secret in `appsettings.json`
- Add cookie authentication with token-bound lifetime
- Show authenticated user's name in the navbar with a logout link
- Add a Logout Razor Page that signs out of both cookie and Keycloak
- Reorder `launchSettings.json` to make the HTTPS profile the default
- No changes to database schema or domain models

## Capabilities

### New Capabilities
- `keycloak-auth`: OpenID Connect authentication via Keycloak, global auth enforcement, session management, and logout

### Modified Capabilities
- *(none)*

## Impact

- `src/WordPicker.Web/Program.cs` — auth service registration and middleware pipeline
- `src/WordPicker.Web/Pages/Shared/_Layout.cshtml` — user info and logout in navbar
- `src/WordPicker.Web/Pages/Logout.cshtml` + `.cshtml.cs` — new logout page
- `src/WordPicker.Web/Properties/launchSettings.json` — profile order
- `src/WordPicker.Web/appsettings.json` — Keycloak configuration section
- `src/WordPicker.Web/WordPicker.Web.csproj` — new NuGet package reference
