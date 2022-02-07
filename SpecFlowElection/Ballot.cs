using System;
using System.Collections.Generic;

namespace SpecFlowElection
{
    [System.Serializable]
    public class NotEnoughUserException : System.Exception
    {
        public NotEnoughUserException() { }
        public NotEnoughUserException(string message) : base(message) { }
    }
    public class Ballot
    {
        protected const int MinCandidates = 3;
        public const string NotEnoughCandidates = "minimum 2 candidates required";
        protected const int MinVoters = 1;
        public const string NotEnoughVoters = "Not enough voters";
        public Ballot() {}


        public Result result = new Result();

        internal Dictionary<Candidate, List<User>> votes = new Dictionary<Candidate, List<User>>();

        protected static Candidate BlankUser = new Candidate("", 666);
        public List<Candidate> Candidates = new List<Candidate>();

        public void Open(List<Candidate> candidates) {
            Candidates = candidates;

            foreach (var c in Candidates) {
                c.ResultRate = 0.00;
            }

            Candidates.Add(BlankUser);
        }

        public void Close()
        {
            if (Candidates.Count < MinCandidates) {
                throw new NotEnoughUserException(NotEnoughCandidates);
            }

            if (Result.FlattenUserInVotes(votes).Count <= MinVoters) {
                throw new NotEnoughUserException(NotEnoughVoters);
            }

            ProcessBallot();
        }

        public void Vote(User voter, string candidateName) {
            Candidate p = Candidates.Find(x => x.Name == candidateName);

            if (candidateName == null || candidateName == "") {
                p = BlankUser;
            }

            if (p == null) {
                Console.Write(candidateName + " doesn't exist");
                return;
            }

            List<User> CurrentList = votes.GetValueOrDefault(p);

            if (CurrentList == null) {
                CurrentList = new List<User>();
                votes.Add(p, CurrentList);
            }

            if (Result.FlattenUserInVotes(votes).Contains(voter)) {
                Console.Write(voter + " already voted");
                return;
            }

            CurrentList.Add(voter);
        }

        protected void ProcessBallot() {
            var toprocessVotes = votes;
            // Reset votes
            votes = new Dictionary<Candidate, List<User>>();
            Open(result.NextRound(toprocessVotes));
        }
    }
}
