
using System;
using System.Linq;
using System.Collections.Generic;

using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowRental.Specs.Steps
{
    class TestRental {
        public int userId;
        public int carId;
        public double days;
        public double estimatedDistance;
    }

    [Binding]
    public class RentalStepBase
    {
        internal readonly RentalBook _rentalBook;

        internal List<Car> _cars;

        internal List<User> _users;

        public RentalStepBase(RentalBook book)
        {
            _rentalBook = book;
            _users = new List<User>();
            _cars = new List<Car>();
        }

        [Given(@"following users not registered")]
        public void GivenFollowingUsersNotRegistered(Table table)
        {
            _users = table.CreateSet<User>().ToList();
        }

        [Given(@"following cars")]
        public void GivenFollowingCars(Table table)
        {
            _cars = table.CreateSet<Car>().ToList();
            _cars.ForEach(c => _rentalBook.AddCar(c));
        }
    }
}
