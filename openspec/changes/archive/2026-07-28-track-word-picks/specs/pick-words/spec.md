## ADDED Requirements

### Requirement: View pick history on a word list
The system SHALL display the last 20 picks for a word list on its detail page, with timestamps.

#### Scenario: Details page shows pick history
- **WHEN** user navigates to a word list's detail page
- **THEN** the page SHALL display the last 20 picks for that list
- **AND** each pick SHALL show the picked word and the timestamp of when it was picked
- **AND** the list SHALL be ordered most recent first

#### Scenario: Details page with no pick history
- **WHEN** user navigates to a word list's detail page and no picks have been made
- **THEN** the page SHALL show an empty state message ("No pick history yet")

#### Scenario: Pick history after word is removed
- **WHEN** a word has been picked and later removed from the list
- **THEN** the corresponding pick history entries SHALL be cascade deleted
