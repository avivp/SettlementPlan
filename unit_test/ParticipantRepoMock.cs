using System;
using System.Collections.Generic;

namespace SettlementPlanApp
{
    class ParticipantRepoMock : IParticipantRepository
    {
        private Dictionary<string, Participant> Participants;
        public void LoadParticipantList()
        {
            Participants = new Dictionary<string, Participant>();
        }

        public bool AddParticipant(Participant p)
        {
            Participants.Add(p.Id, p);
            return true;
        }

        public Participant GetParticipant(string participantId)
        {
            return Participants[participantId];
        }
    }
}