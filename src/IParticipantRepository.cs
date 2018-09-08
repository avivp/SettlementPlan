using System;
namespace SettlementPlanApp
{
  interface IParticipantRepository 
  {
    void LoadParticipantList();
    bool AddParticipant(Participant p);
    Participant GetParticipant(string participantId);
    //bool UpdateParticipant(Participant p);
    //bool RemoveParticipant(Participant p);
  }
}