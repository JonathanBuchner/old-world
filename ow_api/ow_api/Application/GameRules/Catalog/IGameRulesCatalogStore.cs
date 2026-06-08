using ow_gen_lib.Rules;

namespace ow_api.Application.GameRules.Catalog
{
    public interface IGameRulesCatalogStore
    {
        void Set(GameRulesCatalog catalog);
    }
}
