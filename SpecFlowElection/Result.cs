using System.Collections.Generic;
namespace SpecFlowElection
{
    public class Result {
        public const string MIN_CANDIDATES_ERR = "minimum 2 candidates required";
        public const string TOO_MUCH_EQUALS = "Can't continue with 3 equal candidates, restart vote";
        public const string SECOND_ROUND_NEEDED = "Election need a second round";

        protected const string MAX_ROUND = "Can't go further round 2 to determine a winner";

        public string message { get; set; }

        public Candidate winner { get; set; }

        public Dictionary<Candidate, List<User>> leaderboard;

        protected int Round = 1;

        public int GetRound() {
            return Round;
        }

        public Result NextRound() {
            if (winner == null && Round == 1) {
                Round += 1;
                return this;
            }

            throw new System.Exception(MAX_ROUND);
        }

        public string Details() {
            return "";
        }
    }
}
