using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SettlementPlanApp
{
    class SettlementPlanManager 
    {
        private IParticipantRepository ParticipantRepo {get; set;}
        private IPlanRepository PlanRepo { get; set; }
        
        public SettlementPlanManager(IRepositoryFactory factory) 
        {
            ParticipantRepo = factory.CreateParticipantRepository();
            PlanRepo = factory.CreatePlanRepository();
        }

        public SettlementPlan CreateSettlementPlan(string description)
        {
            var plan = new SettlementPlan { Description = description };
            PlanRepo.AddPlan(plan);
            return plan;
        }

        public string GetDebtsSummary(SettlementPlan plan)
        {
            StringBuilder builder = new StringBuilder();
            var list = plan.CalculateDebts();
            foreach (var d in list)
            {
                var p1 = ParticipantRepo.GetParticipant(d.PersonWhoOwes);
                var p2 = ParticipantRepo.GetParticipant(d.PersonWhoGetPayed);
                builder.AppendFormat("{0} needs to pay {1} {2} dollars\n", p1.DisplayName, p2.DisplayName, d.DebtAmount);
            }
            return builder.ToString();
        }

        public void OnShutDown()
        {
            // while participant repo changes very rarely and can be persistent immidiately
            // plans changes quite often, we would like to persist it to datasource only when needed (e.g. shutdown)
            PlanRepo.StoreAllPlans();
        }
    }
}