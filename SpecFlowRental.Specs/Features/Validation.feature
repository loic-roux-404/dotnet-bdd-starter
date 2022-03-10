Feature: Validation

    Background: Validation Data
        # These tests may fail with the time, need to mock clock
        Given following users registered
            | Id | Name             | BornDate   | DriverLicense | Pass |
            | 1  | christian horner | 10/12/1976 | 1             | toto |
            | 2  | toto wolf        | 10/12/1976 |               | toto |
            | 3  | Max Verstappen   | 10/12/1996 | 1             | toto |
            | 4  | Lando Norris     | 10/12/2005 | 1             | toto |
            | 5  | Charles Leclerc  | 12/09/2001 | 1             | toto |
        And following cars
            | Id | Brand   | Model  | Color | KmPrice | Hp |
            | 1  | Honda   | Civic  | black | 0.05    | 7  |
            | 2  | Honda   | Civic  | white | 0.05    | 7  |
            | 3  | Renault | Twingo | grise | 0.02    | 2  |
            | 4  | Ford    | Focus  | white | 0.1     | 12 |
        And following test rentals
            | userId | carId | days | estimatedDistance |
            | 1      | 1     | 10   | 200               |
            | 4      | 2     | 13   | 20                |
            | 2      | 2     | 7    | 29.5              |
            | 3      | 4     | 30   | 421.5             |
            | 5      | 4     | 16   | 200.5             |

    @validation
    @error
    Scenario: Is logged or registered
        When login with user 1
        Then assert not throw
        When rent 1 process
        Then throw validation exception with message Is logged or registered

    @validation
    @error
    Scenario: More than 18year
        When login with user 4
        Then assert not throw
        When rent 2 process
        Then throw validation exception with message More than 18year

    @validation
    @error
    Scenario: Driver license
        When login with user 1
        Then assert not throw
        When rent 1 process
        Then throw validation exception with message Is logged or registered

    @validation
    @error
    Scenario: One car at a time
        When login with user 2
        Then assert not throw
        When rent 3 process
        Then assert not throw
        When rent 3 process
        Then throw validation exception with message One car at a time

    @validation
    @info
    Scenario: Car available
        When login with user 2
        Then assert not throw
        When login with user 4
        Then assert not throw
        When rent 2 process
        Then assert not throw
        When rent 3 process
        Then throw validation exception with message "Car available"

    @validation
    @info
    Scenario: Rent >= 8CV car before 21year
        When login with user 5
        Then assert not throw
        When rent 5 process
        Then throw validation exception with message Is logged or registered
