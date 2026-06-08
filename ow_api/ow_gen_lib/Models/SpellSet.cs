using ow_gen_lib.Enums.Spells;

namespace ow_gen_lib.Models
{
    public class SpellSet
    {
        public List<SpellProfile> SpellProfiles { get; set; } = [];
        public SpellLore SpellLore { get; set; }
    }
}
