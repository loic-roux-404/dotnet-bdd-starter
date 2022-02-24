using System;
using System.Collections.Generic;
using System.Globalization;

namespace SpecFlowRental
{
    public class Rental
    {
        public User User { get; set; }

        public Car Car { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Price { get; set; }

        public bool Closed {get; set;} = false;
    }

    public class ValidateUserException : System.Exception
    {
        public ValidateUserException() { }
        public ValidateUserException(string message) : base(message) { }
    }

    public class RentalBook
    {
        const double BasePrice = 30.00;

        const string ExceedKmErrMsg = "Car not gived back on time, need to adjust price with exceeded km, please retry."
        protected List<Rental> l { get; set; }

        /// <summary> Catalog with is inside rental state </summary>
        protected Dictionary<Car, bool> catalog;
        /// <summary> User and logged state mapping </summary>
        protected Dictionary<User, Boolean> users;

        public void AddCar(Car car, double price)
        {
            catalog.Add(car, false);
        }

        // Possible other criterias
        #nullable enable
        public Car? SearchCar(string brandName) {
            return new List<Car>(catalog.Keys).Find(c => {
                if (c.Brand == brandName) {
                    return true;
                }

                return false;
            });
        }
        #nullable disable

        public void Rent(User u, Car c, DateTime endDate, double estimatedDistance)
        {
            Validate("Is logged or registered", (user, _) => {
                return users.GetValueOrDefault(user) == true;
            }, u, c);

            Validate("More than 18year", (user, _) => {
                return user.BornDate >= DateTime.Now.Subtract(new TimeSpan(365*18, 0, 0, 0));
            }, u, c);

            Validate("Driver license", (user, _) => user.DriverLicense != null, u, c);

            // - once car in same time
            // TODO
            Validate("One car at a time", (user, _) => {
                return l.Find(rental => rental.User == user) == null;
            }, u, c);

            // - car available
            Validate("Car available", (_, car) => {
                // TODO need to do check on date because car could be available
                return l.Find(rental => rental.Car == car) == null;
            }, u, c);

            // - 21y user can't rent >= 8CV car
            Validate("Rent >= 8CV car before 21year", (user, car) => {
                var ismoreThan21y = user.BornDate >= DateTime.Now.Subtract(new TimeSpan(365*21, 0, 0, 0));

                if (ismoreThan21y) return true;

                if (car.Hp >= 8) return false;

                return true;
            }, u, c);

            var r = new Rental();
            r.User = u;
            r.Car = c;
            r.Start = DateTime.Now;
            r.End = endDate;
            r.Price = BasePrice + (c.KmPrice * estimatedDistance);;
            l.Add(r);
            catalog.Add(r.Car, true);
        }
        public void GiveBack(Rental r, double? exceedKm = null)
        {
            if (DateTime.Now >= r.End) {
                if (exceedKm == null) throw new Exception(ExceedKmErrMsg);

                var notNullExceedKm = (double) exceedKm;
                r.Price += r.Car.KmPrice * notNullExceedKm;
                r.End = DateTime.Now;
            }

            r.Closed = true;
            catalog.Add(r.Car, false);
        }

        public void Login(string name, string pass)
        {
            users.Add(FindUserInSession(name), true);
        }

        public void Logout(string name)
        {
            users.Add(FindUserInSession(name), false);
        }

        public void Register(int id,
            string name, string bornDate, int? driverLicense, string pass)
        {
            try {
                var bornD = DateTime.ParseExact(bornDate, "D", new CultureInfo("fr-FR"));
                var u = new User();

                u.Id = id;
                u.Name = name;
                u.BornDate = bornD;
                u.DriverLicense = driverLicense;
                u.Pass = pass;

                users.Add(u, false);
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to parse '{0}'", bornDate);
            }
        }

        public void Validate(string reason, Func<User, Car, bool> callback, User u, Car c)
        {
            if (!callback(u, c))
            {
                throw new ValidateUserException("Failed user validation :" + reason);
            };
        }

        #nullable enable
        protected User? FindUserInSession(string name) {
            var ulist = new List<User>(users.Keys);

            if (ulist.Exists(cu => cu.Name == name)) {
                throw new Exception(name + " not exists in app");
            }

            return ulist.Find(cu => cu.Name == name);
        }
        #nullable disable
    }
}
