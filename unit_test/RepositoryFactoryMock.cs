using System;
namespace SettlementPlanApp
{
  class RepositoryFactoryMock : IRepositoryFactory
  {
      public IParticipantRepository CreateParticipantRepository()
      {
          var repo = new ParticipantRepoMock();
          return repo;
      }
      public IPlanRepository CreatePlanRepository()
      {
          var repo = new PlanRepoMock();
          return repo;
      }
  }
}