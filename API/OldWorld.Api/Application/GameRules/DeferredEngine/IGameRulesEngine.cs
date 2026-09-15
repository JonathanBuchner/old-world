using OldWorld.Domain.Rules;
using OldWorld.Domain.Enums.Army;
using OldWorld.Api.Application.GameRules.DeferredEngine.CombatResolution;

namespace OldWorld.Api.Application.GameRules.DeferredEngine
{
    public interface IGameRulesEngine
    {
        GameVersionEnum GameVersion { get; }
        GameRulesCatalog Catalog { get; }
        CombatResolutionResult CalculateCombatResolution(CombatResolutionRequest request);
    }
}
