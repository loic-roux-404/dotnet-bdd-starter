Feature: Accounting

    Background: Data
        Given following users not registered
            | Id | Name             | BornDate   | DriverLicense |Pass|
            | 1  | christian horner | 10/12/1976 | 1             |toto|
            | 2  | toto wolf        | 10/12/1976 |               |toto|
            | 3  | Max Verstappen   | 10/12/1996 | 1             |toto|
            | 4  | Lando Norris     | 10/12/2005 | 1             |toto|
        And following cars
            | Id | Brand   | Model  | Color | KmPrice | Hp |
            | 1  | Honda   | Civic  | black | 0.05    | 7  |
            | 2  | Honda   | Civic  | white | 0.05    | 7  |
            | 3  | Renault | Twingo | grise | 0.02    | 2  |
            | 3  | Ford    | Focus  | white | 0.1     | 12 |

    @login
    @error
    Scenario: Failed login
        Then login with user 1 throw with message christian horner accounting failed

    @login
    @info
    Scenario: Successful login
        When register users
            |christian horner|
        Then login with user 1 throw with message null

    @logout
    @error
    Scenario: Successful logout
        When register users
            |christian horner|
        Then login with user 1 throw with message null
        Then logout with user 1 throw with message null

    @logout
    @error
    Scenario: Failed logout of a non logged user
        When register users
            |toto wolf|
        Then logout with user 2 throw with message toto wolf accounting failed

    @logout
    @error
    Scenario: Failed logout of a non existing user
        Then logout with user 4 throw with message Lando Norris accounting failed
