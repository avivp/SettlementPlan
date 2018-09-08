using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettlementPlanApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new RepositoryFactory();
            var repo = factory.CreateParticipantRepository();
            repo.LoadParticipantList();
            var bob = new Participant { DisplayName = "Bob", Email = "bob@mymail.com" };
            var sheryl = new Participant { DisplayName = "Sheryl", Email = "sheryl@mymail.com" };
            var dave = new Participant { DisplayName = "Dave", Email = "dave@mymail.com" };
            repo.AddParticipant(bob);
            repo.AddParticipant(sheryl);
            repo.AddParticipant(dave);

            var manager = new SettlementPlanManager(factory); //we could use .Net's Unity here

            var nyPlan = manager.CreateSettlementPlan("trip to NY");
            nyPlan.AddParticipant(bob.Id);
            nyPlan.AddParticipant(sheryl.Id);
            nyPlan.AddParticipant(dave.Id);

            nyPlan.AddCharge(new Charge
            {
                Category = Category.Entertainment,
                Date = new DateTime(),
                TotalCharge = 100,
                DebtByPerson = new List<KeyValuePair<string, double>> { 
                    new KeyValuePair<string, double>(bob.Id, 10), 
                    new KeyValuePair<string, double>(sheryl.Id, 50), 
                    new KeyValuePair<string, double>(dave.Id, 40) },
                Payer = bob.Id
            });
            nyPlan.AddCharge(new Charge
            {
                Category = Category.Dining,
                Date = new DateTime(),
                TotalCharge = 130,
                DebtByPerson = new List<KeyValuePair<string, double>> { 
                    new KeyValuePair<string, double>(bob.Id, 100), 
                    new KeyValuePair<string, double>(dave.Id, 30) },
                Payer = sheryl.Id
            });

            //[bob][sheryl,100]
            //[sheryl] [bob, 50]
            //[dave][bob,40][sheryl,30]

            Console.WriteLine(manager.GetDebtsSummary(nyPlan));
            
            Console.ReadKey();
        }
    }
}
