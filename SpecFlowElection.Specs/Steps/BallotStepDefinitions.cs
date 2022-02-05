
using System;
using System.Collections.Generic;

using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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
            foreach (var h in table.Header) {
                ps.Add(h);
            }

            _ballot.Open(ps);
        }

        [When("ballot close")]
        public void GivenBallotClose()
        {
            _ballot.Close();
        }

        [Given("voter (.*) choose (.*)")]
        public void WhenVoterChoose(string voter, string candidate)
        {
            _ballot.Vote(new User(voter), candidate);
        }

        // TODO replace previous function multiple call
        // Not prio
        // [Given("voters mapping is")]
        // public void WhenVotersVoteMapping(Table table)
        // {
        //     var dictionary = new Dictionary<string, string>();

        //     foreach (var row in table.Rows)
        //     {
        //         dictionary.Add(row[0], row[1]);
        //     }
        // }

        [Then("i have (.*) in candidates")]
        public void ThenIHaveCandidate(string candidateName)
        {
            _ballot.candidates.Should().ContainSingle(
                c => c.Name == candidateName,
                candidateName + " not in candidate names"
            );
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
            if (expected == "null") {
                _ballot.result.winner.Should().BeNull();
            } else {
                _ballot.result.winner.Should().NotBeNull();
            }

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
            var cs = table.CreateSet<Candidate>();
            var ballotCs = _ballot.candidates;

            table.CompareToSet<Candidate>(ballotCs);
        }
    }
}
