using System.Collections.Generic;
using System.Linq;
using ow_gen_lib.Enums.Equipment;

namespace ow_gen_lib.Models.Equipment
{
    public class ArmorProfile : Equipment
    {
        public int ArmorValue { get; set; }

        public int ArmorAdditional { get; set; }

        public List<ArmorSpecialRule> SpecialRules { get; set; } = [];

        public override IReadOnlyList<Enum> GetSpecialRules()
        {
            return SpecialRules.Cast<Enum>().ToList();
        }
    }
}
