using ow_gen_lib.Enums.Army;
using ow_gen_lib.Rules.CombatResolution;

namespace ow_gen_lib.Rules.OldWorldV152Renegade
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
