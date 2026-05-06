using System;
using System.Linq;
using ow_gen_lib.Data.Equipment;
using ow_gen_lib.Models.Equipment;

namespace ow_gen_lib.Factories
{
    public static class EquipmentFactory
    {
        public static ArmorProfile CreateArmor(string nameId, int pointCost)
        {
            if (!ArmorData.Entries.TryGetValue(nameId, out ArmorProfile? armorTemplate))
            {
                throw new ArgumentException($"Unknown armor NameId '{nameId}'.", nameof(nameId));
            }

            return new ArmorProfile
            {
                Name = armorTemplate.Name,
                NameId = armorTemplate.NameId,
                PointCost = pointCost,
                Type = armorTemplate.Type,
                ArmorValue = armorTemplate.ArmorValue,
                ArmorAdditional = armorTemplate.ArmorAdditional,
                SpecialRules = armorTemplate.SpecialRules.ToList()
            };
        }
    }
}
