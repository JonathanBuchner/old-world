using OldWorld.Domain.Enums.Army;
using OldWorld.Domain.Enums.Board;

namespace OldWorld.Domain.Models
{
    public class Game
    {
        public Guid GuidId { get; set; } = Guid.NewGuid();
        public GameVersionEnum GameVersionEnum { get; set; } = GameVersionEnum.Unknown;
        public GameType GameType { get; set; } = GameType.Unknown;
        public List<ArmyBook> ArmyLists { get; set; } = [];
        public ScenarioEnum ScenarioEnum { get; set; } = ScenarioEnum.Unknown;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public Guid? CreatedByUserId { get; set; }
        public string GameNotes { get; set; } = string.Empty;
    }
}
