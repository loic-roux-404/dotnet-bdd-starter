using FluentAssertions;
using TechTalk.SpecFlow;

using System;
using System.Collections.Generic;

namespace SpecFlowBallot.Specs.Steps
{
    [Binding]
    public sealed class BallotStepDefinitions
    {
        private List<Participants> _mockClients;
        private readonly Calculator _calculator;
        private int _result;

        public BallotStepDefinitions(Calculator calculator)
        {
            _calculator = calculator;
        }

        [Given("the following participants:")]
        public void GivenTheFollowingParticipants(List<Participants> participants)
        {
           participants = _mockClients;
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            _calculator.SecondNumber = number;
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            _result = _calculator.Add();
        }

        [When("the two numbers are subtracted")]
        public void WhenTheTwoNumbersAreSubtracted()
        {
            _result = _calculator.Subtract();
        }

        [When("the two numbers are divided")]
        public void WhenTheTwoNumbersAreDivided()
        {
            _result = _calculator.Divide();
        }

        [When("the two numbers are multiplied")]
        public void WhenTheTwoNumbersAreMultiplied()
        {
            _result = _calculator.Multiply();
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            _result.Should().Be(result);
        }
    }
}
