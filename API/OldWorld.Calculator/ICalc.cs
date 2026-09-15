using OldWorld.Calculator.Models;
using OldWorld.Domain.Models.Profile;
using System.Collections.Generic;

namespace OldWorld.Calculator
{
    public interface ICalc
    {
        ToWoundResult ToWound(ModelProfile attacker, List<string> attackerAdditionalRules, ModelProfile defender, List<string> defenderAdditionalRules);

        List<Distribution> TotalBinoDistribution(ToWoundResult result, int attacks);
    }
}
