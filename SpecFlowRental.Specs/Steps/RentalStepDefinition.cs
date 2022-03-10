
using System;
using System.Linq;
using System.Collections.Generic;

using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowRental.Specs.Steps
{
    class TestData {
        public Stack<Action> actionStack;
        public List<User> _users;
        public List<Car> _cars;

        public RentalBook _rentalBook;

        TestData() {
            _rentalBook = new RentalBook();
            _cars = new List<Car>();
            _users = new List<User>();
            actionStack = new Stack<Action>();
        }
    }

    public class TestRental {
        public int userId;
        public int carId;
        public double days;
        public double estimatedDistance;
    }

    [Binding]
    public class AccountingStepDefinitions
    {
        internal RentalBook _rentalBook;
        internal List<User> _users;
        internal List<Car> _cars = new List<Car>();
        public Stack<Action> actionStack;

        internal List<TestRental> _rentals = new List<TestRental>();

        AccountingStepDefinitions(TestData data)
        {
            _rentalBook = data._rentalBook;
            actionStack  = data.actionStack;
            _users = data._users;
            _cars = data._cars;
        }

        [Given(@"non registered users")]
        public void NonRegisteredUsers(Table table)
        {
            _users = table.CreateSet<User>().ToList();
        }

        [Given(@"following users registered")]
        public void GivenFollowingUsersRegistered(Table table)
        {
           _users = table.CreateSet<User>().ToList();
           RegisterUsers(_users.Select(x => x.Name).ToList());
        }

        [Given(@"following users registered and logged")]
        public void GivenFollowingUsersRegisteredAndLogged(Table table)
        {
           _users = table.CreateSet<User>().ToList();
           RegisterUsers(_users.Select(x => x.Name).ToList());
           _users.ForEach(u => _rentalBook.Login(u.Name, u.Pass));
        }

        [When("register users")]
        public void WhenRegisterUsers(Table tusers) {
            var ulist = new List<string>();
            foreach (var usrName in tusers.Header) {
                var u = _users.Find(usr => usr.Name == usrName);
                ulist.Add(u.Name);
            }

            RegisterUsers(ulist);
        }

        [When("login with user (.*)")]
        public void LoginWithUser(int id) {
            var loginPartAction = LoginUserPart(id);

            actionStack.Push(loginPartAction);
        }

        [Then("throw accounting exception with message (.*)")]
        public void ThrowAccountingExceptionWithMessage(string exception) {
            actionStack.Pop().Should().Throw<AccountingException>()
                .WithMessage(exception);
        }

        [When("logout with user (.*)")]
        public void LogoutUser(int id) {
            var u = _users.Find(u => u.Id == id);
            actionStack.Push(_rentalBook.Invoking(y => y.Logout(u.Name)));
        }

        [Given(@"following test rentals")]
        public void GivenFollowingRentals(Table table)
        {
            _rentals = table.CreateSet<TestRental>().ToList();
        }

        [When("rent (.*) process")]
        public void RentProcessThrowWithMessage(int rentIdx) {
            var r = _rentals[rentIdx-1];

            actionStack.Push(_rentalBook.Invoking(invocation => invocation.Rent(
                _users.Find(u => u.Id == r.userId),
                _cars.Find(c => c.Id == r.carId),
                DateTime.Now.AddDays(r.days),
                r.estimatedDistance
            )));
        }

        [Then("throw validation exception with message (.*)")]
        public void AssertThrowValidationExceptionWithMessage(string exception) {
            actionStack.Pop().Should().Throw<ValidateUserRentException>()
                .WithMessage(ValidateUserRentException.MsgPrefix + exception);
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

        [Then("assert not throw")]
        public void AssertNotThrow() {
            actionStack.Pop().Should().NotThrow();
        }

        [Then("throw exception with message (.*)")]
        public void ThrowWithMessage(string exception) {
            actionStack.Pop().Should().Throw<Exception>()
                .WithMessage(exception);
        }

        [When("give back rental of user (.*)")]
        public void GiveBack(int usrid) {
            // TODO find a way to mock clock / Datetime
            actionStack.Push(_rentalBook.Invoking(
                inv => inv.GiveBack(_users.Find(u => u.Id == usrid)))
            );
        }

        [Then("throw exceed km exception")]
        public void ThrowExceedKmException() {
            actionStack.Pop().Should().Throw<Exception>()
                .WithMessage(RentalBook.ExceedKmErrMsg);
        }

        [AfterScenario]
        public void clearAction() {
            actionStack.Clear();
        }

        internal Action LoginUserPart(int id) {
            var u = _users.Find(usr => usr.Id == id);

            return _rentalBook.Invoking(y => y.Login(u.Name, u.Pass));
        }

        internal void RegisterUsers(List<string> userNames) {
            foreach (var usrName in userNames) {
                var u = _users.Find(usr => usr.Name == usrName);

               _rentalBook.Invoking(y => y.Register(u.Id, u.Name, u.BornDate, u.DriverLicense, u.Pass))
                    .Should()
                    .NotThrow();
            }
        }
    }
}
