Feature: Accounting

    Background: Accounting Data
        Given non registered users
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
        When login with user 1
        Then throw accounting exception with message christian horner accounting failed

    @login
    @info
    Scenario: Successful login
        When register users
            |christian horner|
        When login with user 1
        Then assert not throw

    @logout
    @error
    Scenario: Successful logout
        When register users
            |christian horner|
        And login with user 1
        Then assert not throw
        When logout with user 1
        Then assert not throw

    @logout
    @error
    Scenario: Failed logout of a non logged user
        When register users
            |toto wolf|
        When logout with user 2
        Then throw accounting exception with message toto wolf accounting failed

    @logout
    @error
    Scenario: Failed logout of a non existing user
        When logout with user 4
        Then throw accounting exception with message Lando Norris accounting failed
