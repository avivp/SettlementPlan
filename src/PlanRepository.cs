using System;
using System.Collections.Generic;

namespace SettlementPlanApp
{
    // A class that persists plans to some data source (such as DB)
  class PlanRepository : IPlanRepository 
  {
    private Dictionary<string, SettlementPlan> Plans;
    public void LoadPlansList() 
    {
      // read all plans from the repo 
        Plans = new Dictionary<string, SettlementPlan>();
    }

    public void AddPlan(SettlementPlan p)
    {
        Plans.Add(p.Id, p);
    }
    public bool StoreAllPlans() 
    {
      // save Plans map to DB
      return true; //success
    }

    public SettlementPlan GetPlan(string planId)
    {
        return Plans[planId];
    }
  }
}