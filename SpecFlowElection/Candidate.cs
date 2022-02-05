namespace SpecFlowElection
{

    public class Candidate : User
    {
        public float ResultRate { get; set; }
        public Candidate(string name) : base(name) { }

        public string toString() {
            return "{" +
                "Name: " + this.Name +
                "Result: " + this.ResultRate +
            "}";
        }
    }
}
