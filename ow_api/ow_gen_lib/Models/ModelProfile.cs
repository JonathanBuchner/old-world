using System.Collections.Generic;
using System.Linq;
using ow_gen_lib.DataStructures;
using ow_gen_lib.Enums.Bases;
using ow_gen_lib.Enums.Composition;
using ow_gen_lib.Enums.EntityType;
using ow_gen_lib.Enums.TroopType;

namespace ow_gen_lib.Models.Profile
{
    public abstract class ModelProfile : Entry
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

        public int BasePointsValue { get; set; }

        public TroopType TroopType { get; set; }

        public TroopSubtype TroopSubtype { get; set; }

        public CompositionCategory Classification { get; set; }

        public ClassificationSub ClassificationSub { get; set; }

        public BaseSize BaseSize { get; set; }

        public List<Equipment> Equipment { get; set; } = [];

        public List<SpellProfile> Spells { get; set; } = [];

        public List<RuleList> SpecialRules { get; set; } = [];

        public override EntityType EntityType => EntityType.ModelProfile;

        public int TotalPointValue =>
            BasePointsValue +
            Equipment.Sum(equipment => equipment.PointCost) +
            SpecialRules.Sum(specialRule => specialRule.PointCost);
    }
}
