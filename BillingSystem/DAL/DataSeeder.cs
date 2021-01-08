using BillingSystem.Business.Models.TariffPlans;
using System.Collections.Generic;

namespace BillingSystem.DAL
{
    public class DataSeeder
    {

        public DataSeeder()
        {

        }

        private static readonly List<SecondMinuteTariffPlan> TariffPlans = new List<SecondMinuteTariffPlan>();

        //TariffPlans.

        //{ Id = Guid.NewGuid(), PlanName = "Every Second Minute Free" }
    }
}

