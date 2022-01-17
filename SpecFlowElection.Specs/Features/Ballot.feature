Feature: Ballot

@Ballot
Scenario: title
    Given the following participants
        |dupont|
        |jeanne|
        |thierry|
	And the vote process
	When the ballot is closed
	Then the winner is dupont
