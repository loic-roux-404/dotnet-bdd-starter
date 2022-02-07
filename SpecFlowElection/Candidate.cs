namespace SpecFlowElection
{

    public class Candidate : User
    {
        public double ResultRate { get; set; }
        public Candidate(string name, int id) : base(name, id) { }

        public string toString() {
            return "{" +
                "Name: " + this.Name +
                "Result: " + this.ResultRate +
            "}";
        }
    }
}
