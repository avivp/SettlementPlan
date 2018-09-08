using System;
namespace SettlementPlanApp
{
  interface IRepositoryFactory
  {
      IParticipantRepository CreateParticipantRepository();
      IPlanRepository CreatePlanRepository();
  }
}