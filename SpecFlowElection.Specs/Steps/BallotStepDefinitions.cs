
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

        private List<Candidate> _candidates;

        private List<User> _users;

        public BallotStepDefinitions(Ballot ballot)
        {
            _ballot = ballot;
            _users = new List<User>();
            _candidates = new List<Candidate>();
        }

        [Given(@"following candidates")]
        public void GivenFollowingCandidates(Table table)
        {
            foreach (var h in table.Header) {
                _candidates.Add(new Candidate(h, _candidates.Count + _users.Count + 1));
            }
        }
        [Given(@"following voters")]
        public void GivenFollowingUsers(Table table)
        {
            foreach (var h in table.Header) {
                _users.Add(new User(h, _candidates.Count + _users.Count + 1));
            }
        }

        [Given("classic test ballot")]
        public void GivenClassicTestBallot() {
            GivenFollowingCandidates(new Table("dupont","jeanne", "thierry", "fab"));
            GivenFollowingUsers(new Table("toto", "titi", "tata", "tutu", "tete", "kiki"));
            _ballot.Open(_candidates);
        }

        [When("ballot open")]
        public void GivenBallotOpen()
        {
            _ballot.Open(_candidates);
        }

        [When("ballot close")]
        public void GivenBallotClose()
        {
            _ballot.Close();
        }

        [Given("voter (.*) choose (.*)")]
        public void WhenVoterChoose(string voter, string candidate)
        {
            var voterFound = _users.Find(v => v.Name == voter);

            if (candidate == "<null>") {
                candidate = null;
            }

            _ballot.Vote(voterFound, candidate);
        }

        [Then("i have (.*) in candidates")]
        public void ThenIHaveCandidate(string candidateName)
        {
            _ballot.Candidates.Should().ContainSingle(
                c => c.Name == candidateName,
                candidateName + " not in candidate names"
            );
        }

        [Then("result details matching (.*)")]
        public void WhenResultDetailsMatching(string details)
        {
            _ballot.result.details.Should().Be(details);
        }

        [Then("result winner name is (.*)")]
        public void ThenResultWinnerNameIs(string expected)
        {
            if (expected == "null") {
                _ballot.result.winner.Should().BeNull();
                return;
            }

            var winner = _ballot.result.winner;

            winner.Should().NotBeNull(expected);
            winner.Name.Should().Be(expected);
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

        [Then("close and exception message is (.*)")]
        public void WhenCloseAndExceptionMessageIs(string messageExpected)
        {
            _ballot.Invoking(y => y.Close())
                .Should().Throw<System.Exception>()
                .WithMessage(messageExpected);
        }

        [Then(@"check remains only two candidates")]
        public void ThenCheckRemainsOnlyTwoCandidates(Table table)
        {
            var cs = table.CreateSet<Candidate>();
            var ballotCs = _ballot.Candidates;
            StringUtils.Debug(ballotCs);

            table.CompareToSet<Candidate>(ballotCs);
        }
    }
}
