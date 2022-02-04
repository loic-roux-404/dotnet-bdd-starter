using System;
using System.Collections.Generic;

using FluentAssertions;
using TechTalk.SpecFlow;
namespace SpecFlowElection.Specs.Steps
{
    [Binding]
    public sealed class BallotStepDefinitions
    {
        private readonly Ballot _ballot;

        public BallotStepDefinitions(Ballot ballot)
        {
            _ballot = ballot;
        }

        [Given(@"following candidates")]
        public void GivenFollowingCandidates(Table table)
        {
            List<string> ps = new List<string>();
            // TODO create participant list from table
            // Table participants = table;
            foreach (var row in table.Rows) {
                ps.Add(row[0]);
            }
            Console.WriteLine("aaaaa", _ballot);
            _ballot.Open(ps);
        }

        [When(@"ballot close")]
        public void GivenBallotClose()
        {
            _ballot.Close();
        }

        [Given("voter (.*) choose (.*)")]
        public void WhenVoterChoose(string voter, string candidate)
        {
            _ballot.Vote(voter, candidate);
        }

        // TODO replace previous function multiple call
        // Not prio
        [Given("voters mapping is")]
        public void WhenVotersVoteMapping(Table table)
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
        }


        [Then("result details matching (.*)")]
        public void WhenResultDetailsMatching(string details)
        {
            // TODO create detail field in result and add string to this during vote process
            // _result = //
        }

        [Then("result winner name is (.*)")]
        public void ThenResultWinnerNameIs(string expected)
        {
            _ballot.result.Should().NotBeNull();

            string _name = _ballot.result.winner.Name;
            _name.Should().Be(expected);
        }

        [Then("result round is (.*)")]
        public void ThenResultRoundIs(int expected)
        {
            _ballot.result.Should().NotBeNull();

            int _round = _ballot.result.GetRound();
            _round.Should().Be(expected);
        }

        [Then("result message matching (.*)")]
        public void WhenResultMatchingMessage(string messageExpected)
        {
            _ballot.result.Should().NotBeNull();

            string _result = _ballot.result.message;
            _result.Should().Be(messageExpected);
        }

        [Then(@"check remains only two candidates")]
        public void ThenCheckRemainsOnlyTwoCandidates(Table table)
        {
            // TODO search if expected candidates in table are in list and table rows are equals (eliminated loosers)
            // Use Fluent assertion to validate
        }
    }
}
