using ow_gen_lib.Enums.EntityType;
using ow_gen_lib.Enums.Spells;

namespace ow_gen_lib.Models
{
    public abstract class SpellProfile : Entity
    {
        public SpellType Type { get; set; }
        public SpellLore Lore { get; set; }
        public int RequiredRoll { get; set; }
        public int Range { get; set; }
        public override EntityType EntityType => EntityType.Spell;
    }
}
