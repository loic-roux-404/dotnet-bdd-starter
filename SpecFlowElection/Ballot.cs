using System;
using System.Collections.Generic;

namespace SpecFlowBallot
{
    public class Ballot
    {
        public string Reason { get; set; }

        public int SecondNumber { get; set; }

        public bool Done = false;

        private List<Participant> participants = new List<Participant>();

        public void Close()
        {
            Done = true;
        }

        public int Subtract()
        {
            return FirstNumber - SecondNumber;
        }

        public int Divide()
        {
            if (FirstNumber == 0 || SecondNumber == 0) return 0;

            return FirstNumber / SecondNumber;
        }

        public int Multiply()
        {
            return FirstNumber * SecondNumber;
        }
    }
}
