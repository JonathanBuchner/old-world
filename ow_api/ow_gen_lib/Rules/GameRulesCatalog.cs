using ow_gen_lib.Enums.Army;
using ow_gen_lib.Models;

namespace ow_gen_lib.Rules
{
    public class GameRulesCatalog
    {
        public GameVersionEnum GameVersion { get; set; } = GameVersionEnum.Unknown;
        public IReadOnlyDictionary<string, Rule> Rules { get; set; } = new Dictionary<string, Rule>();
        public IReadOnlyDictionary<string, Equipment> Equipment { get; set; } = new Dictionary<string, Equipment>();
        public IReadOnlyDictionary<string, SpellSet> Lores { get; set; } = new Dictionary<string, SpellSet>();
    }
}
