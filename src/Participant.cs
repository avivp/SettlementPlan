using System;
namespace SettlementPlanApp
{
  class Participant 
  {
    public string Id {get; private set;}
    public string DisplayName {get; set;}
    public string Email {get; set;}

    public Participant() {
        Id = IdGenerator.GenerateUniqueId();
    }

    // null object design pattern
    public static Participant Empty()
    {
        return new Participant { DisplayName = "Unknown Participant"};
    }
  }
}