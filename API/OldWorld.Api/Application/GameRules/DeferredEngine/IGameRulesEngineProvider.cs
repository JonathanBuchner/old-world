using OldWorld.Domain.Enums.Army;

namespace OldWorld.Api.Application.GameRules.DeferredEngine
{
    public interface IGameRulesEngineProvider
    {
        IGameRulesEngine Get(GameVersionEnum gameVersion);
    }
}
