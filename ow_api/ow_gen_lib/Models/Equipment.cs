using System.Collections.Generic;
using ow_gen_lib.DataStructures;
using ow_gen_lib.Enums.EntityType;
using ow_gen_lib.Enums.Equipment;

namespace ow_gen_lib.Models
{
    public class Equipment : Entry
    {
        public int RemainingUses { get; set; }
        public EquipmentType Type { get; set; }
        public EquipmentSubType SubType { get; set; }
        public EquipmentClassification Classification { get; set; }
        public EquipmentUses Uses { get; set; }
        public override EntityType EntityType => EntityType.Equipment;
        public RuleList Rules { get; set; } = new RuleList();
    }
}
