using System;
namespace SettlementPlanApp
{
  class RepositoryFactory : IRepositoryFactory
  {
      private IParticipantRepository ParticipantRepo { get; set; }
      private IPlanRepository PlanRepo { get; set; }

      public IParticipantRepository CreateParticipantRepository()
      {
          // lazy ceation and load of repo. this should be protected by mutex in case of multithread.
          if (ParticipantRepo == null)
          {
              ParticipantRepo = new ParticipantRepository();
              ParticipantRepo.LoadParticipantList();
          }

          return ParticipantRepo;
      }
      public IPlanRepository CreatePlanRepository()
      {
          // lazy ceation and load of repo. this should be protected by mutex in case of multithread.
          if (PlanRepo == null)
          {
              PlanRepo = new PlanRepository();
              PlanRepo.LoadPlansList();
          }
          
          return PlanRepo;
      }
  }
}