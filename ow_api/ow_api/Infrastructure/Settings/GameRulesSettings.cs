using System.ComponentModel.DataAnnotations;
using ow_gen_lib.Enums.Army;

namespace ow_api.Infrastructure.Settings
{
    public class GameRulesSettings
    {
        [Required]
        public GameVersionEnum DefaultGameVersion { get; set; } = GameVersionEnum.Unknown;

        [Required]
        [MinLength(1)]
        public List<GameVersionEnum> SupportedGameVersions { get; set; } = [];

        [Required]
        public string RulesContainerName { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public Dictionary<GameVersionEnum, string> VersionFolders { get; set; } = [];
    }
}
