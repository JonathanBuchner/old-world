using System.Collections.Generic;
using ow_gen_lib.Enums.Equipment;

namespace ow_gen_lib.Models.Equipment
{
    public abstract class Equipment
    {
        public string NameId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int PointCost { get; set; }

        public EquipmentType Type { get; set; }


        public abstract IReadOnlyList<Enum> GetSpecialRules();
    }
}
