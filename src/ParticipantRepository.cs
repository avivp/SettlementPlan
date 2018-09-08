using System;
using System.Collections.Generic;

namespace SettlementPlanApp
{
    // A class that persists participants to some data source (such as DB)
  class ParticipantRepository : IParticipantRepository 
  {
    private Dictionary<string, Participant> Participants;
    public void LoadParticipantList() 
    {
      // read all users from the repo 
      Participants = new Dictionary<string, Participant>();
    }

    public bool AddParticipant(Participant p) 
    {
      Participants.Add(p.Id, p);
      // save to DB
      return true; //success
    }

    public Participant GetParticipant(string participantId)
    {
        if (!Participants.ContainsKey(participantId))
        {
            return Participant.Empty();
        }
        return Participants[participantId];
    }
  }
}