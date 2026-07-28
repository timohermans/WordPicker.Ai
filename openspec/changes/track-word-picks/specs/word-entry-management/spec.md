## MODIFIED Requirements

### Requirement: Remove a word from a list
The system SHALL allow users to remove a word from a word list.

#### Scenario: Successful word removal
- **WHEN** user confirms removal of a word from a list
- **THEN** system deletes the WordEntry record and its associated pick history, then refreshes the list detail page

#### Scenario: Remove confirmation required
- **WHEN** user clicks the remove button next to a word
- **THEN** system displays a confirmation prompt before removing the word
