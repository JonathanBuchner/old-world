using System.Collections.Generic;
using System.Linq;
using OldWorld.Domain.DataStructures;
using OldWorld.Domain.Enums.Bases;
using OldWorld.Domain.Enums.Composition;
using OldWorld.Domain.Enums.EntityType;
using OldWorld.Domain.Enums.TroopType;

namespace OldWorld.Domain.Models.Profile
{
    public class ModelProfile : Entry
    {
        public int Movement { get; set; }
        public int WeaponSkill { get; set; }
        public int BallisticSkill { get; set; }
        public int Strength { get; set; }
        public int Toughness { get; set; }
        public int Wounds { get; set; }
        public int Initiative { get; set; }
        public int Attacks { get; set; }
        public int Leadership { get; set; }
        public List<Equipment> Equipment { get; set; } = [];
        public List<SpellProfile> Spells { get; set; } = [];
        public List<RuleList> SpecialRules { get; set; } = [];
        public override EntityType EntityType => EntityType.ModelProfile;
        public int TotalPointValue =>
          PointCost +
          Equipment.Sum(equipment => equipment.PointCost) +
          SpecialRules.Sum(specialRule => specialRule.PointCost);
    }
}
