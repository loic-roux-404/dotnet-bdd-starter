
using System;
using System.Linq;
using System.Collections.Generic;

using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowRental.Specs.Steps
{
    [Binding]
    [Scope(Feature = "Accounting")]
    public sealed class AccountingStepDefinitions: RentalStepBase
    {
        public AccountingStepDefinitions(RentalBook book): base(book) {}

        const string ValidationMsgFailed = "Failed user validation :";


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

        [Given(@"following users not registered")]
        public void GivenFollowingUsersNotRegistered(Table table)
        {
            _users = table.CreateSet<User>().ToList();
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

        internal Action LoginUserPart(int id, string exception = "") {
            var u = _users.Find(usr => usr.Id == id);
            if (exception == "null") {
                _rentalBook.Invoking(y => y.Login(u.Name, u.Pass)).Should().NotThrow();

                return null;
            };

            return _rentalBook.Invoking(y => y.Login(u.Name, u.Pass));
        }

        private void RegisterUsers(List<string> userNames) {
            foreach (var usrName in userNames) {
                var u = _users.Find(usr => usr.Name == usrName);
                _rentalBook.Register(u.Id, u.Name, u.BornDate, u.DriverLicense, u.Pass);
            }
        }

        [Then("login with user (.*) throw with message (.*)")]
        public void LoginWithUser(int id, string exception = "") {
            var loginPartAction = LoginUserPart(id, exception);
            if (loginPartAction == null) return;

            loginPartAction.Should().Throw<AccountingException>()
                .WithMessage(exception);
        }

        [Then("user validation (.*) throw with message (.*)")]
        public void UserValidationThrowWithMessage(int id, string exception = "") {
            var loginPartAction = LoginUserPart(id, exception);
            if (loginPartAction == null) return;

            loginPartAction.Should().Throw<ValidateUserException>()
                .WithMessage(ValidationMsgFailed + exception);
        }

        [Then("logout with user (.*) throw with message (.*)")]
        public void LogoutUserThrowWithMessage(int id, string exception = "") {
            var u = _users.Find(u => u.Id == id);
            var action = _rentalBook.Invoking(y => y.Logout(u.Name));

            if (exception == "null")
                action.Should().NotThrow();

            action.Should().Throw<AccountingException>()
                .WithMessage(exception);
        }
    }
}
