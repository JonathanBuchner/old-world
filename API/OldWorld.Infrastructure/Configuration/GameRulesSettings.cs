using System.ComponentModel.DataAnnotations;
using OldWorld.Domain.Enums.Army;

namespace OldWorld.Infrastructure.Configuration
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
