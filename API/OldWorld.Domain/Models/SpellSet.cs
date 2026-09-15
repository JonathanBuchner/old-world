using OldWorld.Domain.Enums.Spells;

namespace OldWorld.Domain.Models
{
    public class SpellSet
    {
        public List<SpellProfile> SpellProfiles { get; set; } = [];
        public SpellLore SpellLore { get; set; }
    }
}
