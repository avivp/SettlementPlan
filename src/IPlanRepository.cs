using System;
namespace SettlementPlanApp
{
  interface IPlanRepository 
  {
    void LoadPlansList();
    void AddPlan(SettlementPlan p);
    bool StoreAllPlans();
    SettlementPlan GetPlan(string planId);
  }
}