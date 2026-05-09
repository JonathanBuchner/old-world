using ow_gen_lib.Models;
using ow_gen_lib.Models.Profile;
using System.Collections.Generic;

namespace ow_gen_lib.Interfaces
{
    public interface ICalc
    {
        ToWoundResult ToWound(
            ModelProfile attacker,
            List<string> attackerAdditionalRules,
            ModelProfile defender,
            List<string> defenderAdditionalRules);

        List<Distribution> TotalBinoDistribution(ToWoundResult result, int attacks);
    }
}
