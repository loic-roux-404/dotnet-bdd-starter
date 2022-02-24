
using System;
using System.Linq;
using System.Collections.Generic;

using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowRental.Specs.Steps
{
    [Binding]
    public sealed class RentalStepDefinitions
    {
        const string ValidationMsgFailed = "Failed user validation :";
        private readonly RentalBook _rentalBook;

        private List<Car> _cars;

        private List<User> _users;

        public RentalStepDefinitions(RentalBook book)
        {
            _rentalBook = book;
            _users = new List<User>();
            _cars = new List<Car>();
        }

        [Given(@"following cars")]
        public void GivenFollowingCars(Table table)
        {
            _cars = table.CreateSet<Car>().ToList();
            Console.Write(_cars.Count);
        }

        [Given(@"following users")]
        public void GivenFollowingUsers(Table table)
        {
           _users = table.CreateSet<User>().ToList();;
        }

        [When("login with user (.*) with exception message (.*)")]
        public void LoginWithUser(int id, string exception = "") {
            var u = _users.Find(usr => usr.Id == id);
            if (exception == "") {
                _rentalBook.Login(u.Name, u.Pass);

                return;
            };

            _rentalBook.Invoking(y => y.Login(u.Name, u.Pass))
                .Should().Throw<ValidateUserException>()
                .WithMessage(ValidationMsgFailed + exception);
        }

        [When("register with users")]
        public void RegisterUsers(Table table) {
            foreach (var usrName in table.Header) {
                var u = _users.Find(usr => usr.Name == usrName);
                _rentalBook.Register(u.Id, u.Name, u.BornDate.ToString(), u.DriverLicense, u.Pass);
            }
        }
    }
}
