using System;

namespace SettlementPlanApp
{
    class IdGenerator
    {
        // point of improvement: Guid might return the same id that is already in use
        // better to implement IdGenerator class with a state
        public static string GenerateUniqueId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
