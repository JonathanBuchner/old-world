using OldWorld.Domain.Enums.Army;
using OldWorld.Domain.Enums.Spells;

namespace OldWorld.Domain.Models
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
