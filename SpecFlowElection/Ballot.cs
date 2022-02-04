using System;
using System.Collections.Generic;

namespace SpecFlowElection
{
    public class Ballot
    {
        public Ballot() {}

        public Result result;

        public bool IsDone() {
            return result != null || result.winner != null;
        }

        private List<Participant> participants = new List<Participant>();
        private Dictionary<Participant, List<string>> votes = new Dictionary<Participant, List<string>>();

        public void Open(List<string> candidatesNameList) {
            candidatesNameList.ForEach(x => {
                Participant p = new Participant();
                p.Name = x;
                participants.Add(p);
            });
        }

        public void Close()
        {
            // TODO here process votes from dict
            Result result = new Result();
            result.message = "Oui";
        }

        public void Vote(string voter, string participantName) {
            Participant p = participants.Find(x => x.Name == participantName);

            if (p == null) {
                return;
            }

            List<string> CurrentList = votes.GetValueOrDefault(p);

            if (CurrentList == null) {
                CurrentList = new List<string>();
            }

            CurrentList.Add(participantName);
            votes.Add(p, CurrentList);
        }
    }
}
