using ow_gen_lib.Enums.Army;
using ow_gen_lib.Rules.CombatResolution;

namespace ow_gen_lib.Rules
{
    public interface IGameRulesEngine
    {
        GameVersionEnum GameVersion { get; }
        GameRulesCatalog Catalog { get; }
        CombatResolutionResult CalculateCombatResolution(CombatResolutionRequest request);
    }
}
