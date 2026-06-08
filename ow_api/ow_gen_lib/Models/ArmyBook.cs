using ow_gen_lib.Enums.Army;
using ow_gen_lib.Enums.Spells;

namespace ow_gen_lib.Models
{
    public class ArmyBook
    {
        public GameVersionEnum GameVersion { get; set; } = GameVersionEnum.Unknown;
        public FactionEnum Faction { get; set; }
        public List<Model> ModelEntries { get; set; } = [];
        public List<Rule> SpecialRules { get; set; } = [];
        public List<Equipment> MagicItems { get; set; } = [];
        public List<SpellSet> Lores { get; set; } = [];
    }
}
