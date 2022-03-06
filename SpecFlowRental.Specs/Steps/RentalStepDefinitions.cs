
using System;
using System.Linq;
using System.Collections.Generic;

using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowRental.Specs.Steps
{
    [Binding]
    [Scope(Feature = "Validation")]
    [Scope(Feature = "Accounting")]
    [Scope(Feature = "Rental")]
    public sealed class RentalDefinitions : AccountingStepDefinitions
    {
        public RentalDefinitions(RentalBook book): base(book) {}

        internal List<TestRental> _rentals = new List<TestRental>();

        [Given(@"following test rentals")]
        public void GivenFollowingRentals(Table table)
        {
            _rentals = table.CreateSet<TestRental>().ToList();
        }

        [Then("rent (.*) process throw with message (.*)")]
        public void RentProcessThrowWithMessage(int rentIdx, string exception = "") {
            var r = _rentals[rentIdx-1];
            _rentalBook.Invoking(invocation => invocation.Rent(
                    _users.Find(u => u.Id == r.userId),
                    _cars.Find(c => c.Id == r.carId),
                    DateTime.Now.AddDays(r.days),
                    r.estimatedDistance
                ))
                .Should().Throw<ValidateUserRentException>()
                .WithMessage(ValidateUserRentException.MsgPrefix + exception);
        }
    }
}
