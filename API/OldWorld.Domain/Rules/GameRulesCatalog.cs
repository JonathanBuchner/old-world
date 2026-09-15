using OldWorld.Domain.Enums.Army;
using OldWorld.Domain.Models;

namespace OldWorld.Domain.Rules
{
    public class GameRulesCatalog
    {
        public GameVersionEnum GameVersion { get; set; } = GameVersionEnum.Unknown;
        public IReadOnlyDictionary<string, Rule> Rules { get; set; } = new Dictionary<string, Rule>();
        public IReadOnlyDictionary<string, Equipment> Equipment { get; set; } = new Dictionary<string, Equipment>();
        public IReadOnlyDictionary<string, SpellSet> Lores { get; set; } = new Dictionary<string, SpellSet>();
    }
}
