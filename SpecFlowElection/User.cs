using static System.Guid;

namespace SpecFlowElection {

    public class User {
        public string Name { get; }
        public System.Guid Id { get; }

        public User(string name) {
            Name = name;
            Id = System.Guid.NewGuid();
        }
    }
}
