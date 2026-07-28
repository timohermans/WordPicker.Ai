## ADDED Requirements

### Requirement: Create a new word list
The system SHALL allow users to create a new word list by providing a name.

#### Scenario: Successful word list creation
- **WHEN** user submits the new list form with a valid name (1-200 characters)
- **THEN** system creates a new WordList record and redirects to the list index page

#### Scenario: Validation error on empty name
- **WHEN** user submits the new list form with an empty or whitespace-only name
- **THEN** system displays a validation error and returns to the form

### Requirement: View all word lists
The system SHALL display all existing word lists on an index page.

#### Scenario: Lists are displayed
- **WHEN** user navigates to the word lists index page
- **THEN** system shows all word lists with their name and creation date, sorted by most recent

#### Scenario: No lists exist
- **WHEN** user navigates to the index page with no word lists in the database
- **THEN** system displays an appropriate empty state message

### Requirement: View a single word list with its words
The system SHALL allow users to view a specific word list and all words it contains.

#### Scenario: List with words displayed
- **WHEN** user navigates to a word list's detail page
- **THEN** system shows the list name and all words in that list

#### Scenario: List not found
- **WHEN** user navigates to a word list that does not exist
- **THEN** system returns a 404 not found response

### Requirement: Delete a word list
The system SHALL allow users to delete an existing word list.

#### Scenario: Successful deletion
- **WHEN** user confirms deletion of a word list
- **THEN** system removes the word list and all its associated words, then redirects to the index page

#### Scenario: Delete confirmation required
- **WHEN** user clicks the delete button on a word list
- **THEN** system displays a confirmation prompt before proceeding with deletion
