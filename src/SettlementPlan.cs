using System;
using System.Collections.Generic;
using System.Linq;
namespace SettlementPlanApp
{
    // points of improvement:
    // We do not support modifying a charge that was added to the plan
    // All charges are the same currency
    class SettlementPlan 
    {
        private IList<string> Participants {get; set;}
        private IList<Charge> Charges {get; set;}
        public string Description {get; set;}
        public string Id {get; private set;}
        private CashFlowCalculator CashFlow;
        private Dictionary<string, Dictionary<string, double>> DebtTracking;

        public SettlementPlan() 
        {
            Participants = new List<string>();
            Charges = new List<Charge>();
            Id = IdGenerator.GenerateUniqueId();
            CashFlow = new CashFlowCalculator();
            DebtTracking = new Dictionary<string, Dictionary<string, double>>();
        }

        public void AddCharge(Charge g)
        {
            ValidateCharge(g);
            Charges.Add(g);
            ProcessDebtsFromCharge(g);
        }

        public void AddParticipant(string participantId)
        {
            Participants.Add(participantId);
        }

        // process the debts once it is request, which might take some time. 
        // we could also process the debt upon AddCharge.
        public List<DebtInfo> CalculateDebts() 
        {
            var debts = BuildDebtsMatrix(); //there's some redundant copies and creation of matrices but it's to keep CashFlowCalculator as an independent component 
            
            var result = CashFlow.CalculateCashFlow(debts, Participants.Count);

            return BuildDebtInfoList(result);
        }

        private void ValidateCharge(Charge g)
        {
            var notExistIds = g.DebtByPerson.Select(x => x.Key).Where(x => !Participants.Contains(x));
            if (notExistIds.Any())
            {
                throw new ArgumentException("Failed to add a charge to settlement plan. The following participants were not added to plan : {0}", String.Join(",", notExistIds));
            }
        }

        private void ProcessDebtsFromCharge(Charge g)
        {
            foreach (var debt in g.DebtByPerson)
            {
                // the person in debt.Key needs to pay debt.Value to the charge's Payer
                var participantId = debt.Key;
                if (participantId.Equals(g.Payer))
                {
                    continue;   //ignore when payer owes themselves
                }
                if (!DebtTracking.ContainsKey(participantId))
                {
                    DebtTracking.Add(participantId, new Dictionary<string, double>());
                }
                var participantDebts = DebtTracking[participantId];
                if (!participantDebts.ContainsKey(g.Payer))
                {
                    participantDebts.Add(g.Payer, debt.Value);
                }
                else
                {
                    participantDebts[g.Payer] += debt.Value;    // at this moment we do not support modify existing charge
                }
            }
        }

        private double[,] BuildDebtsMatrix()
        {
            // process the debts - create a map of the amount 1 person owes an other
            var debts = new double[Participants.Count, Participants.Count];

            for (var i = 0; i < Participants.Count; i++)
            {
                for (var j = 0; j < Participants.Count; j++)
                {
                    //person owes 0 to themselves
                    if (i == j) continue;

                    // there might be no debts between person i and j
                    if (!DebtTracking.ContainsKey(Participants[i]) ||
                        !DebtTracking[Participants[i]].ContainsKey(Participants[j])) continue;

                    debts[i, j] = DebtTracking[Participants[i]][Participants[j]];   // get the debt that person i owes person j
                }
            }
            return debts;
        }

        private List<DebtInfo> BuildDebtInfoList(double[,] debts)
        {
            var count = Participants.Count;
            var result = new List<DebtInfo>(count);
            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < count; j++)
                {
                    if (debts[i,j] > 0)
                    {
                        result.Add(new DebtInfo
                        {
                            PersonWhoOwes = Participants[i],
                            PersonWhoGetPayed = Participants[j],
                            DebtAmount = debts[i, j]
                        });
                    }
                }
            }
            return result;
        }
    
    }
}