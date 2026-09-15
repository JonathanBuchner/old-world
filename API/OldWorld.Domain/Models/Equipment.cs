using System.Collections.Generic;
using OldWorld.Domain.DataStructures;
using OldWorld.Domain.Enums.EntityType;
using OldWorld.Domain.Enums.Equipment;

namespace OldWorld.Domain.Models
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
