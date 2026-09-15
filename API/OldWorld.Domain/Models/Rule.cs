using OldWorld.Domain.Enums.SpecialRules;
using EntityTypeEnum = OldWorld.Domain.Enums.EntityType.EntityType;

namespace OldWorld.Domain.Models
{
    public class Rule : Entry
    {
        public override EntityTypeEnum EntityType => EntityTypeEnum.Rules;
        public RuleEffect RuleEffect { get; set; }
    }
}
