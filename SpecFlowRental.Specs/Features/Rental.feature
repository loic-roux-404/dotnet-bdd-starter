Feature: Rental

# Background: Given following user login and rentals
    Background: Data
        Given following users registered and logged
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
