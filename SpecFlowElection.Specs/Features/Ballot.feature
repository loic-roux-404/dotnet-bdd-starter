Feature: Ballot

    @ballot_simple
    @ballot_error
    Scenario: simple add participants
        Given following candidates
            |dupont|jeanne|thierry|
        Then i have dupont in candidates
        When ballot close
        Then result message matching No participation

    @ballot_simple
    @ballot_error
    Scenario: minimum de deux candidat requis
        Given following candidates
            |dupont|
        When ballot close
        Then result message matching minimum 2 candidates required

    @ballot_simple
    @ballot_info
    Scenario: Simple vote with absolut majority
        Given following candidates
            |dupont|jeanne|thierry|
        And voter toto choose dupont
        And voter titi choose jeanne
        And voter tata choose dupont
        When ballot close
        Then result message matching dupont winned in first turn

    @ballot_simple
    @ballot_warning
    Scenario: Same user voted twice
        Given following candidates
            |dupont|jeanne|thierry|
        And voter toto choose dupont
        And voter toto choose jeanne
        Then result message matching user can't vote twice

    # possible scenatio, try create ballot candidate with null name
    # Special name used for blank vote

    @ballot_simple
    @ballot_warning
    Scenario: Blank vote
        Given following candidates
            |dupont|jeanne|thierry|
        Then i have dupont in candidates
        Given voter tata choose ''
        And voter toto choose jeanne
        And voter titi choose jeanne
        And voter tutu choose thierry
        And voter tata choose thierry
        When ballot close
        # Jeanne win because tata has voted blank and can't vote twice
        Then result winner name is jeanne

    @ballot_simple
    @ballot_info
    Scenario: List results with percent
    Given the following candidates
        |dupont|jeanne|thierry|
    And voter toto choose dupont
    And voter titi choose jeanne
    And voter tata choose dupont
    When ballot close
    Then result details matching dupont: 66.66% / Jeanne 33.33%

    @ballot_simple
    @ballot_info
    @winner
    Scenario: Vainqueur du scrutin in first round
        Given following candidates
            |dupont|jeanne|thierry|
        And voter toto choose dupont
        And voter titi choose jeanne
        And voter tata choose dupont
        And voter tata choose thierry
        When ballot close
        Then result message matching Winner is dupont

    @ballot_special
    @ballot_info
    Scenario: Second row with winner
        Given following candidates
            |dupont|jeanne|thierry|
        # Could create a data table
            # | voter| candidate |
            # | toto | dupont  |
            # | tutu | dupont  |
            # | titi | jeanne  |
            # | tata | jeanne  |
            # | tete | thierry |
        And voter toto choose dupont
        And voter tutu choose jeanne
        And voter titi choose jeanne
        And voter tata choose thierry
        And voter tete choose thierry
        When ballot close
        Then result winner name is ''
        Then result message matching Election need a second round
        Then check remains only two candidates
            |jeanne|thierry|
        Given voter tutu choose jeanne
        And voter titi choose jeanne
        And voter tata choose thierry
        And voter tete choose jeanne
        When ballot close
        Then result winner name is jeanne
        Then result message matching info : Winner is jade

    @ballot_special
    @ballot_error
    Scenario: Second row with no winner
        Given following candidates
            |dupont|jeanne|thierry|fab|
        And voter toto choose dupont
        And voter tutu choose jeanne
        And voter titi choose jeanne
        And voter tata choose thierry
        And voter tete choose thierry
        When ballot close
        Then result winner name is null
        Then result round is 2
        Then result message matching Election need a second round
        And check remains only two candidates
            | sku | Name    |
            | 1   | jeanne  |
            | 2   | thierry |
        Given voter tutu choose jeanne
        And voter titi choose jeanne
        And voter tata choose thierry
        And voter tete choose thierry
        When ballot close
        Then result winner name is null
        Then result message matching Can't go further round 2 to determine a winner

    @ballot_special
    @ballot_error
    Scenario: 3 candidates equals
        Given following candidates
            |dupont|jeanne|thierry|fab|
        And voter toto choose dupont
        And voter kiki choose dupont
        And voter tutu choose jeanne
        And voter titi choose jeanne
        And voter tata choose thierry
        And voter tete choose thierry
        When ballot close
        Then result winner name is null
        Then result message matching Can't continue with 3 equal candidates, restart vote
