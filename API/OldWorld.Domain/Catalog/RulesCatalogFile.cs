using OldWorld.Domain.Enums.Army;
using OldWorld.Domain.Models;

namespace OldWorld.Domain.Catalog
{
    public class RulesCatalogFile
    {
        public GameVersionEnum GameVersion { get; set; } = GameVersionEnum.Unknown;
        public List<Rule> Rules { get; set; } = [];
    }
}
