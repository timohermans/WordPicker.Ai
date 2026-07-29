## Context

WordPicker is an ASP.NET Core 10 Razor Pages app with no authentication. All pages are public. The app uses EF Core with PostgreSQL. Keycloak is already running at `https://<redacted>.nl` with realm `home`.

## Goals / Non-Goals

**Goals:**
- Authenticate all users via Keycloak using OpenID Connect Code Flow
- Require authentication on every page globally (no opt-out, including error page)
- Bind the authentication cookie lifetime to the Keycloak token's lifetime
- Show the authenticated user's name in the navbar with a logout link
- Provide a logout page that signs out of both the local cookie and the Keycloak session
- Make the HTTPS launch profile the default

**Non-Goals:**
- Per-user word lists or data isolation (future concern)
- Role-based authorization or admin pages
- Custom login page (Keycloak handles the login UI)
- User registration or self-service (handled by Keycloak)

## Decisions

### 1. OIDC package: Microsoft.AspNetCore.Authentication.OpenIdConnect
Microsoft's own package. No third-party dependencies. The OIDC handler handles the code flow, token exchange, and cookie management.

### 2. Global auth: AuthorizeFolder("/")
Single `options.Conventions.AuthorizeFolder("/")` on the Razor Pages options applies `[Authorize]` to every page. The `/Error` page is intentionally included — anonymous users don't deserve error pages if they can't authenticate.

### 3. Cookie lifetime: UseTokenLifetime = true (default)
The OIDC handler's `UseTokenLifetime` (defaults to `true`) sets the cookie's `Expires` to match the ID token's `exp` claim. No `ExpireTimeSpan` override needed. The cookie lives exactly as long as Keycloak says the token is valid.

### 4. User info from Keycloak
`GetClaimsFromUserInfoEndpoint = true` fetches the user's name, email, etc. from Keycloak's `/userinfo` endpoint after token exchange. The `name` claim is displayed in the navbar.

### 5. Logout as a dedicated Razor Page
A new `/Logout` page calls `SignOutAsync` for both `CookieAuthenticationDefaults.AuthenticationScheme` and `OpenIdConnectDefaults.AuthenticationScheme`, then redirects to `/`. This ensures the user is signed out of both the app cookie and the Keycloak session.

### 6. HTTPS as default launch profile
Reorder `launchSettings.json` so the `https` profile appears first. The dropdown in IDEs and the CLI default to the first profile. .NET's dev cert handles TLS locally.

### 7. Client ID/secret in appsettings.json
The `Keycloak` section lives in `appsettings.json` with the client ID and secret. For production, these can be overridden via environment variables or user secrets. The `Authority` URL auto-discovers the OIDC metadata — no `MetadataAddress` or `RequireHttpsMetadata` config needed since it's real HTTPS.

## Risks / Trade-offs

| Risk | Mitigation |
|---|---|
| Keycloak unreachable → all pages inaccessible | The app itself still serves; auth just fails at the OIDC challenge. No infinite loops — the browser gets a connection error from Keycloak. |
| Token expires mid-session | `UseTokenLifetime` means the cookie expires with the token. The user is silently redirected to Keycloak for re-authentication. |
| No anonymous access at all → cannot even see an error page | Mitigated by user preference: "Anonymous users don't deserve errors." If needed later, `/Error` can be unblocked. |
