
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
        }
        [Given(@"following users")]
        public void GivenFollowingUsers(Table table)
        {
           _users = table.CreateSet<User>().ToList();;
        }
    }
}
