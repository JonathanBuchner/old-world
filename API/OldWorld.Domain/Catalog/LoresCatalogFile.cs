using OldWorld.Domain.Enums.Army;
using OldWorld.Domain.Models;

namespace OldWorld.Domain.Catalog
{
    public class LoresCatalogFile
    {
        public GameVersionEnum GameVersion { get; set; } = GameVersionEnum.Unknown;
        public List<SpellSet> Lores { get; set; } = [];
    }
}
