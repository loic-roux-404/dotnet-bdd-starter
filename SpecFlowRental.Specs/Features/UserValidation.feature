Feature: UserValidation

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

    # @validation
    # @error
    # Scenario: Failed login
    #     Then login with user 1 throw with message christian horner not exists in app

   # @validation
    #@info
    #Scenario: sc
        # When register users
        #     |christian horner|
        # Then login with user 1 throw with message null
