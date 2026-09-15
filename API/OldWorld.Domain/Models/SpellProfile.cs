using OldWorld.Domain.Enums.EntityType;
using OldWorld.Domain.Enums.Spells;

namespace OldWorld.Domain.Models
{
    public class SpellProfile : Entry
    {
        public SpellType Type { get; set; }
        public SpellLore Lore { get; set; }
        public int RequiredRoll { get; set; }
        public int Range { get; set; }
        public override EntityType EntityType => EntityType.Spell;
    }
}
