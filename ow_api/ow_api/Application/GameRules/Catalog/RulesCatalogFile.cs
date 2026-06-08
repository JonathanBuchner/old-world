using ow_gen_lib.Enums.Army;
using ow_gen_lib.Models;

namespace ow_api.Application.GameRules.Catalog
{
    public class RulesCatalogFile
    {
        public GameVersionEnum GameVersion { get; set; } = GameVersionEnum.Unknown;
        public List<Rule> Rules { get; set; } = [];
    }
}
