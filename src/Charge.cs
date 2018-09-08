using System;
using System.Collections.Generic;
namespace SettlementPlanApp
{
  class Charge 
  {
    public double TotalCharge {get; set;}
    public DateTime Date {get; set;}
    public Category Category {get; set;}
    public string Description {get; set;}
    public List<KeyValuePair<string, double>> DebtByPerson {get; set;} // id of person and amount they need to pay to Payer
    public string Payer {get; set;}
  }
}