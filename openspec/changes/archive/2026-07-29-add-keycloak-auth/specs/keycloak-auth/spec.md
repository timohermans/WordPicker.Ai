## ADDED Requirements

### Requirement: User can authenticate via Keycloak
The system SHALL authenticate users via Keycloak using OpenID Connect Code Flow.
Unauthenticated requests to any page SHALL redirect to Keycloak's login page.
After successful authentication, the system SHALL create a local authentication cookie bound to the token lifetime.

#### Scenario: Unauthenticated user is redirected to Keycloak
- **WHEN** an unauthenticated user requests any page
- **THEN** the system redirects the user to the Keycloak login page

#### Scenario: Authenticated user accesses a page
- **WHEN** an authenticated user (valid cookie) requests any page
- **THEN** the system serves the page normally

### Requirement: Cookie lifetime matches Keycloak token
The authentication cookie's expiration SHALL be tied to the Keycloak ID token's `exp` claim.
When the token expires, the cookie SHALL expire, requiring re-authentication.

#### Scenario: Session ends at token expiry
- **WHEN** the Keycloak ID token expires
- **THEN** the authentication cookie also expires
- **THEN** the next request redirects the user to Keycloak for re-authentication

### Requirement: User can sign out
The system SHALL provide a logout page that signs the user out of both the local cookie and the Keycloak session.
After sign-out, the system SHALL redirect the user to the home page.

#### Scenario: User initiates logout
- **WHEN** an authenticated user navigates to the Logout page
- **THEN** the system clears the local authentication cookie
- **THEN** the system signs the user out of the Keycloak session
- **THEN** the system redirects the user to the home page

### Requirement: Navbar shows user info and logout link
The navbar SHALL display the authenticated user's name and a logout link when the user is signed in.

#### Scenario: Authenticated user sees user info
- **WHEN** an authenticated user is on any page
- **THEN** the navbar displays the user's name
- **THEN** the navbar displays a Logout link

#### Scenario: Unauthenticated user does not see user info
- **WHEN** an unauthenticated user views a page (should not occur after global auth is applied)
- **THEN** the system redirects to Keycloak before rendering the page

### Requirement: Application runs on HTTPS by default
The system SHALL default to the HTTPS launch profile.
The HTTPS profile SHALL be the first profile listed in `launchSettings.json`.

#### Scenario: Development launch
- **WHEN** the application is launched in development mode
- **THEN** the default profile SHALL be the HTTPS profile
