using System.Collections.Generic;
using System.Linq;
using ow_gen_lib.Enums.Equipment;

namespace ow_gen_lib.Models.Equipment
{
    public class WeaponProfile : Equipment
    {
        public int MinRange { get; set; }

        public int MaxRange { get; set; }

        public StrType StrengthType { get; set; }

        public int Strength { get; set; }

        public int StrengthPlus { get; set; }

        public int ArmourPiercing { get; set; }

        public int ArmorBane { get; set; }

        public List<WeaponSpecialRule> SpecialRules { get; set; } = [];

        public override IReadOnlyList<Enum> GetSpecialRules()
        {
            return SpecialRules.Cast<Enum>().ToList();
        }
    }
}
