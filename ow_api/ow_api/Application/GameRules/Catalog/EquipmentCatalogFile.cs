using ow_gen_lib.Enums.Army;
using ow_gen_lib.Models;

namespace ow_api.Application.GameRules.Catalog
{
    public class EquipmentCatalogFile
    {
        public GameVersionEnum GameVersion { get; set; } = GameVersionEnum.Unknown;
        public List<Equipment> EquipmentEntries { get; set; } = [];
    }
}
