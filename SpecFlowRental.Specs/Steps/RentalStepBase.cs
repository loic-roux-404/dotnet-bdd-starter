
using System;
using System.Linq;
using System.Collections.Generic;

using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowRental.Specs.Steps
{
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

        [Given(@"following cars")]
        public void GivenFollowingCars(Table table)
        {
            _cars = table.CreateSet<Car>().ToList();
            _cars.ForEach(c => _rentalBook.AddCar(c));
        }
    }
}
