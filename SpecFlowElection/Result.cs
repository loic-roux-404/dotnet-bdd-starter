namespace SpecFlowElection
{
    public enum Level {
        Info,
        Warning,
        Error
    }

    public class Result {

        public string message { get; set; }

        public Participant winner { get; set; }

        protected int Round = 1;

        public int GetRound() {
            return Round;
        }

        public Result NextRound() {
            if (winner == null && Round == 1) {
                Round += 1;
                return this;
            }

            throw new System.Exception("Can't go further round 2 to determine a winner");
        }
    }
}
