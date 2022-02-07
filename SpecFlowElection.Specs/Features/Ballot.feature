Feature: Ballot

    @ballot_simple
    @ballot_error
    Scenario: simple add participants
        Given following candidates
            | dupont | jeanne |
        And following voters
            | toto |
        When ballot open
        Then i have dupont in candidates
        And close and exception message is Not enough voters

    @ballot_simple
    @ballot_error
    Scenario: minimum de deux candidat requis
        Given following candidates
            | dupont |
        And following voters
            | toto | tata |
        When ballot open
        Given voter toto choose dupont
        Then close and exception message is minimum 2 candidates required

    @ballot_simple
    @ballot_info
    Scenario: Simple vote with absolut majority
        Given classic test ballot
        And voter toto choose dupont
        And voter titi choose jeanne
        And voter tata choose dupont
        When ballot close
        Then result winner name is dupont

    @ballot_simple
    @ballot_warning
    Scenario: Same user voted twice
        Given classic test ballot
        And voter toto choose dupont
        And voter toto choose jeanne
        Then close and exception message is Not enough voters

    # # possible scenatio, try create ballot candidate with null name
    # # Special name used for blank vote

    @ballot_simple
    @ballot_warning
    Scenario: Blank vote
        Given classic test ballot
        Then i have dupont in candidates
        Given voter tata choose null
        And voter toto choose jeanne
        And voter titi choose jeanne
        And voter kiki choose jeanne
        And voter tutu choose thierry
        And voter tata choose thierry
        When ballot close
        # Jeanne win because tata has voted blank and can't vote twice
        Then result winner name is jeanne

    @ballot_simple
    @ballot_info
    Scenario: List results with percent
        Given classic test ballot
        And voter toto choose dupont
        And voter titi choose jeanne
        And voter tata choose dupont
        When ballot close
        Then result details matching | dupont: 66.67 | jeanne: 33.33 |

    @ballot_special
    @ballot_error
    Scenario: Second round with no winner
        Given classic test ballot
        And voter toto choose dupont
        And voter tutu choose jeanne
        And voter titi choose jeanne
        And voter tata choose thierry
        And voter tete choose thierry
        When ballot close
        Then result winner name is null
        Then result round is 1
        Then result message matching Election need a second round
        And check remains only two candidates
            | Id  | Name    |
            | 2   | jeanne  |
            | 3   | thierry |
            | 666 |         |
        Given voter tutu choose jeanne
        And voter titi choose jeanne
        And voter tata choose thierry
        And voter tete choose thierry
        When ballot close
        Then result round is 2
        Then result winner name is null
        Given voter tata choose thierry
        Given voter kiki choose thierry
        Then close and exception message is Can't go further round 2 to determine a winner

    @ballot_special
    @ballot_info
    Scenario: Second round with equals in second round
        Given classic test ballot
        And voter toto choose dupont
        And voter tutu choose jeanne
        And voter titi choose jeanne
        And voter tata choose thierry
        And voter tete choose thierry
        When ballot close
        Then result winner name is null
        Then result round is 1
        Then result message matching Election need a second round
        And check remains only two candidates
            | Id  | Name    |
            | 2   | jeanne  |
            | 3   | thierry |
            | 666 |         |
        Given voter kiki choose jeanne
        And voter tutu choose jeanne
        And voter titi choose jeanne
        And voter tata choose thierry
        And voter tete choose thierry
        When ballot close
        Then result winner name is jeanne

    @ballot_special
    @ballot_error
    Scenario: Equals candidate in first round
        Given classic test ballot
        And voter kiki choose dupont
        And voter toto choose dupont
        And voter tutu choose jeanne
        And voter titi choose jeanne
        And voter tata choose thierry
        And voter tete choose thierry
        Then close and exception message is Can't continue with equal candidates, restart vote
        And result winner name is null
        And result round is 1
