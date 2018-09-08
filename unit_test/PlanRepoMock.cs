using System;
using System.Collections.Generic;

namespace SettlementPlanApp
{
    class PlanRepoMock : IPlanRepository
    {
        public void LoadPlansList()
        {
            // some mock implementation
        }

        public void AddPlan(SettlementPlan p)
        {
            // some mock implementation
        }

        public bool StoreAllPlans()
        {
            // some mock implementation
            return true;
        }

        public SettlementPlan GetPlan(string planId)
        {
            return new SettlementPlan();
        }
    }
}
