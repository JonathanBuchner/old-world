using OldWorld.Domain.Enums.Army;
using OldWorld.Domain.Models;

namespace OldWorld.Domain.Catalog
{
    public class EquipmentCatalogFile
    {
        public GameVersionEnum GameVersion { get; set; } = GameVersionEnum.Unknown;
        public List<Equipment> EquipmentEntries { get; set; } = [];
    }
}
