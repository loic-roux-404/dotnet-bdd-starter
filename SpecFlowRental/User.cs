using System;

namespace SpecFlowRental {

    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public int? DriverLicense { get; set; }
        public string Pass { get; set; }
    }
}
