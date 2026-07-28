# Pick Words

## Purpose

Allow users to select one or more word lists and pick a single random word from each selected list, displayed on the same page.

## Requirements

### Requirement: Random word picking from selected lists
The system SHALL allow a user to select one or more word lists and pick a single random word from each selected list.

#### Scenario: Navigate to pick page from word lists index
- **WHEN** user is on the word lists overview page
- **THEN** they SHALL see a "Pick words" button
- **AND** clicking it SHALL navigate to `/WordLists/Pick`

#### Scenario: Pick page shows all word lists as checkboxes
- **WHEN** user navigates to `/WordLists/Pick`
- **THEN** the page SHALL display all existing word lists as selectable checkboxes
- **AND** each checkbox SHALL show the list name
- **AND** there SHALL be a "Pick" (or "Pick words") submit button
- **AND** there SHALL be a "Cancel" / "Back" link to return to the index

#### Scenario: Pick page with no word lists
- **WHEN** user navigates to `/WordLists/Pick` and no word lists exist
- **THEN** the page SHALL show an empty state message ("No word lists available")
- **AND** SHALL NOT show the submit button

#### Scenario: Select lists, pick words, and see results on the same page
- **WHEN** user checks one or more lists and clicks the pick button
- **THEN** the system SHALL pick one random word per selected list
- **AND** the same page SHALL display the results inline below the selection form
- **AND** each result SHALL show the list name and the randomly selected word
- **AND** there SHALL be a "Pick again" button to reset back to the selection view

#### Scenario: Selected list has no words
- **WHEN** a selected list has no word entries
- **THEN** the results SHALL show "No words in this list" for that list instead of a word

#### Scenario: No lists selected
- **WHEN** user clicks the pick button without selecting any lists
- **THEN** the system SHALL NOT pick words
- **AND** SHALL show a validation message ("Select at least one list")

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
