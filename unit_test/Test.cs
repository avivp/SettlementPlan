using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SettlementPlanApp.UnitTests
{
    [TestFixture]
    public class TestSettlePlan
    {
        [Test]
        public void CalcCashFlow()
        {
            var factory = new RepositoryFactoryMock();
            var repo = factory.CreateParticipantRepository();
            var bob = new Participant { DisplayName = "Bob", Email = "bob@mymail.com" };
            var sheryl = new Participant { DisplayName = "Sheryl", Email = "sheryl@mymail.com" };
            var dave = new Participant { DisplayName = "Dave", Email = "dave@mymail.com" };
            repo.AddParticipant(bob);
            repo.AddParticipant(sheryl);
            repo.AddParticipant(dave);

            var manager = new SettlementPlanManager(factory);

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

            var result = nyPlan.CalculateDebts();

            //bob owes 10 to cheryl
            Assert.AreEqual(result[0].PersonWhoOwes, bob.Id);
            Assert.AreEqual(result[0].PersonWhoGetPayed, sheryl.Id);
            Assert.AreEqual(result[0].DebtAmount, 10.0);

            //dave owes 70 to cheryl
            Assert.AreEqual(result[1].PersonWhoOwes, dave.Id);
            Assert.AreEqual(result[1].PersonWhoGetPayed, sheryl.Id);
            Assert.AreEqual(result[1].DebtAmount, 70.0);
        }
    }
}
