using OldWorld.Domain.Rules;
using OldWorld.Domain.Enums.Army;
using OldWorld.Api.Application.GameRules.DeferredEngine.CombatResolution;

namespace OldWorld.Api.Application.GameRules.DeferredEngine.OldWorldV152Renegade
{
    public class OldWorldV152RenegadeRulesEngine : IGameRulesEngine
    {
        private readonly IGameRulesCatalogProvider _gameRulesCatalogProvider;

        public OldWorldV152RenegadeRulesEngine(IGameRulesCatalogProvider gameRulesCatalogProvider)
        {
            _gameRulesCatalogProvider = gameRulesCatalogProvider;
        }

        public GameVersionEnum GameVersion => GameVersionEnum.OldWorldV152Renegade;
        public GameRulesCatalog Catalog => _gameRulesCatalogProvider.Get(GameVersion);

        public CombatResolutionResult CalculateCombatResolution(CombatResolutionRequest request)
        {
            return new CombatResolutionResult();
        }
    }
}
