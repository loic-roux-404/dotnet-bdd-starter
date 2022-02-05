using System.Collections.Generic;
using System.Linq;
using System;

namespace SpecFlowElection
{
    public class Ballot
    {
        const int EQUALS_FOR_RESTART = 3;
        const int EQUALS_FOR_SECOND_ROUND = 2;
        const int EQUALS_NO_ONE = 1;

        public Ballot() {}

        public Result result = new Result();

        internal Dictionary<Candidate, List<User>> votes = new Dictionary<Candidate, List<User>>();

        public List<Candidate> candidates = new List<Candidate>();
        public void Open(List<string> candidatesNameList) {
            candidatesNameList.ForEach(x => candidates.Add(new Candidate(x)));
        }

        public bool IsDone() {
            return result != null || result.winner != null;
        }

        public void Close()
        {
            if (candidates.Count < 2) {
                result.message = Result.MIN_CANDIDATES_ERR;
                return;
            }

            ProcessBallot();

            // TODO here process votes from dict
            result.message = "Oui";
        }

        public void Vote(User voter, string candidateName) {
            Candidate p = candidates.Find(x => x.Name == candidateName);

            if (p == null) {
                Console.Write(candidateName + " doesn't exist");
                return;
            }

            List<User> CurrentList = votes.GetValueOrDefault(p);

            if (CurrentList == null) {
                CurrentList = new List<User>();
                votes.Add(p, CurrentList);
            }

            CurrentList.Add(voter);
        }

        protected void ProcessBallot() {
            result.leaderboard = GetLeaderBoard(votes);

            var equals = GetEquals(result.leaderboard);

            if (equals.Count == EQUALS_FOR_RESTART) {
                votes = new Dictionary<Candidate, List<User>>();
                result.message = Result.TOO_MUCH_EQUALS;
                return;
            }

            if (equals.Count == EQUALS_FOR_SECOND_ROUND) {
                result.message = Result.SECOND_ROUND_NEEDED;
                result.leaderboard = new Dictionary<Candidate, List<User>>();
                candidates = equals;

                try {
                    result.NextRound();
                } catch(Exception e) {
                    result.message = e.Message;
                }
                return;
            }

            if (equals.Count == EQUALS_NO_ONE) {
                result.leaderboard = ProcessCandidateRates(result.leaderboard);
                // TODO process majority correctly with rates in candidates in new leaderboard
            }
        }

        protected List<Candidate> GetEquals(Dictionary<Candidate, List<User>> l) {
            var equals = new List<Candidate>();

            foreach (var c in l) {
                if (c.Value.Count == l.First().Value.Count) {
                    equals.Add(c.Key);
                }
            }

            return equals;
        }

        protected Dictionary<Candidate, List<User>> ProcessCandidateRates(Dictionary<Candidate, List<User>> leaderboard) {
            var totalParticipants = FlattenUserInCandidates(leaderboard);
            var leaderBoardWithRates = new Dictionary<Candidate, List<User>>();

            foreach (var c in leaderboard) {
                c.Key.ResultRate = c.Value.Count / totalParticipants.Count;
                leaderBoardWithRates.Add(c.Key, c.Value);
            }

            return leaderBoardWithRates;
        }

        protected List<User> FlattenUserInCandidates(Dictionary<Candidate, List<User>> candidates) {
            var users = new List<User>();
            foreach (var c in candidates) {
                users.AddRange(c.Value);
            }

            return users;
        }

        protected Dictionary<Candidate, List<User>> GetLeaderBoard(Dictionary<Candidate, List<User>> l) {
            return (from entry in l orderby entry.Value.Count descending select entry)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        protected void CreateBlankParticipant() {
            candidates.Add(new Candidate(""));
        }
    }
}
