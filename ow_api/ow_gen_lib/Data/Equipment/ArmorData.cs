using System.Collections.Generic;
using ow_gen_lib.Enums.Equipment;
using ow_gen_lib.Models.Equipment;

namespace ow_gen_lib.Data.Equipment
{
    public static class ArmorData
    {
        public static readonly IReadOnlyDictionary<string, ArmorProfile> Entries =
            new Dictionary<string, ArmorProfile>
            {
                ["LightArmor"] = new ArmorProfile
                {
                    Name = "Light Armor",
                    NameId = "LightArmor",
                    Type = EquipmentType.Armor,
                    ArmorValue = 6,
                    ArmorAdditional = 0,
                    SpecialRules = []
                },
                ["HeavyArmor"] = new ArmorProfile
                {
                    Name = "Heavy Armor",
                    NameId = "HeavyArmor",
                    Type = EquipmentType.Armor,
                    ArmorValue = 5,
                    ArmorAdditional = 0,
                    SpecialRules = []
                },
                ["Plate"] = new ArmorProfile
                {
                    Name = "Plate Armor",
                    NameId = "Plate",
                    Type = EquipmentType.Armor,
                    ArmorValue = 4,
                    ArmorAdditional = 0,
                    SpecialRules = []
                },
                ["Shield"] = new ArmorProfile
                {
                    Name = "Shield",
                    NameId = "Shield",
                    Type = EquipmentType.Armor,
                    ArmorValue = 0,
                    ArmorAdditional = 1,
                    SpecialRules = []
                }
            };
    }
}
