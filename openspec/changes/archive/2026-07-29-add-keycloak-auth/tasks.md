## 1. Package and Configuration Setup

- [x] 1.1 Add `Microsoft.AspNetCore.Authentication.OpenIdConnect` NuGet package
- [x] 1.2 Add `Keycloak` section to `appsettings.json` with `Authority`, `ClientId`, and `ClientSecret`
- [x] 1.3 Reorder `launchSettings.json` to make the `https` profile the first entry

## 2. Auth Services and Middleware

- [x] 2.1 Register authentication services in `Program.cs`: `AddAuthentication(CookieDefaults)` + `AddCookie()` + `AddOpenIdConnect`
- [x] 2.2 Add `app.UseAuthentication()` to the middleware pipeline before `UseAuthorization()`
- [x] 2.3 Apply global auth: `options.Conventions.AuthorizeFolder("/")` on `AddRazorPages()`

## 3. Navbar User Info

- [x] 3.1 Add user info section to `_Layout.cshtml` navbar: display `User.Identity.Name` and a Logout link

## 4. Logout Page

- [x] 4.1 Create `Pages/Logout.cshtml` with minimal content (e.g., signing out message)
- [x] 4.2 Create `Pages/Logout.cshtml.cs` with `OnGet` that calls `SignOutAsync` for both cookie and OIDC schemes, then redirects to `/`
