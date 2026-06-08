using ow_gen_lib.Enums.SpecialRules;
using EntityTypeEnum = ow_gen_lib.Enums.EntityType.EntityType;

namespace ow_gen_lib.Models
{
    public class Rule : Entry
    {
        public override EntityTypeEnum EntityType => EntityTypeEnum.Rules;
        public RuleEffect RuleEffect { get; set; }
    }
}
