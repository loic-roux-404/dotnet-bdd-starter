using System;
using System.Collections.Generic;

namespace SpecFlowRental
{
    public class RentalDateRange {
        Tuple<DateTime, DateTime> data { get; set;}
    }
    public class Rental {
        Tuple<User, Car> data { get; set;}
    }
    public class Agenda {
        Dictionary<RentalDateRange, Rental> data { get; set;}
    }

    public class ValidateUserException : System.Exception
    {
        public ValidateUserException() { }
        public ValidateUserException(string message) : base(message) { }
    }

    public class RentalBook
    {
        const double BasePrice = 30.00;
        protected Agenda agenda;

        /// <summary> Price for a car concerned in a rental </summary>
        protected Dictionary<Car, double> RentalPrices;
        /// <summary> User and logged state mapping </summary>
        protected Dictionary<User, Boolean> users;

        public void AddBookSlot() {

        }

        public void Rent(User u, Car c, double estimatedDistance) {
            // Validation required here
            // - is registered and logged
            // - once car in same time
            // - available
            // - 21y user can't rent >= 8CV car
            // - need >= 18y
            // - need driver license
        }
        public void GiveBack(Car c) {
            DateTime d = new DateTime();
            // Find rental in agenda and use date
            // test if milisecond are > to estimated
            // if (d.Millisecond > rentalEndDate)
        }

        public void Login() {

        }

        public void Register() {

        }

        public void Logout() {

        }

        public void Validate(string reason, Func<User, bool> callback, User u) {
            if (!callback(u)) {
                throw new ValidateUserException("Failed user validation :" + reason);
            };
        }
    }
}
