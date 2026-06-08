using ow_gen_lib.Enums.Army;

namespace ow_gen_lib.Rules
{
    public interface IGameRulesEngineProvider
    {
        IGameRulesEngine Get(GameVersionEnum gameVersion);
    }
}
