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
        Then login with user 1 throw with message null
        And rent 1 process throw with message Is logged or registered

    @validation
    @error
    Scenario: More than 18year
        Then login with user 4 throw with message null
        And rent 2 process throw with message More than 18year

    @validation
    @error
    Scenario: Driver license
        Then login with user 1 throw with message null
        And rent 1 process throw with message Is logged or registered

    @validation
    @error
    Scenario: One car at a time
        Then login with user 1 throw with message null
        And test rent 1 process throw with message null
        And rent 1 process throw with message One car at a time

    @validation
    @info
    Scenario: Car available
        Then login with user 2 throw with message null
        Then login with user 4 throw with message null
        And rent 2 process throw with message null
        And rent 3 process throw with message "Car available"

    @validation
    @info
    Scenario: Rent >= 8CV car before 21year
        Then login with user 5 throw with message null
        And rent 5 process throw with message Is logged or registered
