Feature: User validation

    Background: Data
        Given following users
            | Id | Name             | BornDate   | DriverLicense |
            | 1  | christian horner | 10/12/1976 | 1             |
            | 2  | toto wolf        | 10/12/1976 |               |
        And following cars
            | id | brand   | model  | color | KmPrice | Hp |
            | 1  | Honda   | Civic  | black | 0.05    | 7  |
            | 2  | Honda   | Civic  | white | 0.05    | 7  |
            | 3  | Renault | Twingo | grise | 0.02    | 2  |
            | 3  | Ford    | Focus  | white | 0.1     | 12 |
