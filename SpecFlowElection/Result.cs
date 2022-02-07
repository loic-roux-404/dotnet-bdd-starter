using System.Collections.Generic;
using System.Linq;
using System;

namespace SpecFlowElection
{
    public class ErrorResultException : System.Exception
    {
        public ErrorResultException() { }
        public ErrorResultException(string message) : base(message) { }
    }

    public class RoundException : System.Exception
    {
        public RoundException() { }
        public RoundException(string message) : base(message) { }
    }

    public class Result
    {
        public const string TooMuchEquals = "Can't continue with 3 equal candidates, restart vote";
        public const string SecondRoundNeeded = "Election need a second round";

        public const string MaxRound = "Can't go further round 2 to determine a winner";
        public const string BlankWinner = "blank vote majority, restart vote.";

        const int EqualsForRestart = 3;
        const int EqualsForSecondRound = 2;
        const int EqualsNoOne = 1;

        const double RateForWin = 50.00;

        public string message { get; set; }
        public string details { get; set; }

        public Candidate winner { get; set; }

        protected int Round = 0;

        public int GetRound()
        {
            return Round;
        }

        public List<Candidate> NextRound(Dictionary<Candidate, List<User>> votes)
        {
            IncrementRound();

            var leaderboard = GetLeaderBoard(votes);
            details = Details(leaderboard);
            var newCandidates = new List<Candidate>();

            StringUtils.Debug(Result.FlattenCandidatesInVotes(leaderboard));
            StringUtils.Debug(Result.FlattenUserInVotes(leaderboard));

            var first = leaderboard.FirstOrDefault().Key;

            if (first.ResultRate > RateForWin) {
                winner = first;
                message = "Winner is " + winner.Name;
                return newCandidates;
            }

            // Prepare candidates for next row
            newCandidates.Add(first);
            newCandidates.Add(leaderboard.ElementAt(1).Key);
            message = SecondRoundNeeded;

            return newCandidates;
        }

        protected void IncrementRound()
        {
            if (Round <= 1)
            {
                Round += 1;
                return;
            }

            throw new RoundException(MaxRound);
        }

        protected Dictionary<Candidate, List<User>> ProcessCandidateRates(Dictionary<Candidate, List<User>> leaderboard)
        {
            var totalParticipants = FlattenUserInVotes(leaderboard);
            var leaderBoardWithRates = new Dictionary<Candidate, List<User>>();

            foreach (var c in leaderboard)
            {
                c.Key.ResultRate = ((double) c.Value.Count / (double) totalParticipants.Count) * 100;
                leaderBoardWithRates.Add(c.Key, c.Value);
            }

            return leaderBoardWithRates;
        }

        public static List<User> FlattenUserInVotes(Dictionary<Candidate, List<User>> votes)
        {
            var users = new List<User>();

            foreach (var c in votes)
            {
                users.AddRange(c.Value);
            }

            return users;
        }

        public static List<Candidate> FlattenCandidatesInVotes(Dictionary<Candidate, List<User>> votes)
        {
            var candidates = new List<Candidate>();

            foreach (var c in votes)
            {
                candidates.Add(c.Key);
            }

            return candidates;
        }

        protected Dictionary<Candidate, List<User>> GetLeaderBoard(Dictionary<Candidate, List<User>> l) {
            return (from entry in ProcessCandidateRates(l) orderby entry.Key.ResultRate descending select entry)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        protected List<Candidate> GetEquals(Dictionary<Candidate, List<User>> leaderboard)
        {
            var equals = new List<Candidate>();

            foreach (var c in leaderboard)
            {
                if (c.Value.Count == leaderboard.First().Value.Count)
                {
                    equals.Add(c.Key);
                }
            }

            return equals;
        }

        public string Details(Dictionary<Candidate, List<User>> leaderboard)
        {
            var res = "";

            foreach (var l in ProcessCandidateRates(leaderboard))
            {
                res += "| " + l.Key.Name + ": " + Math.Round(l.Key.ResultRate, 2) + " ";
            }

            return res + "|";
        }
    }
}
