using System.Numerics;
using ow_gen_lib.DataStructures;
using ow_gen_lib.Enums.SpecialRules;
using ow_gen_lib.Models;

namespace ow_odds_lib.Calc
{
    public static class Calc_ChanceToWard
    {
        private class ChanceToSucceedWardParams
        {
            public int WardSave { get; set; }
            public RuleList ArmorRules { get; set; } = new RuleList();
            public RuleList OpponentRules { get; set; } = new RuleList();
        }

        public static RollStat ChanceToNotWardSave(int wardSave, RuleList wardRules, RuleList opponentRules)
        {
            var p = new ChanceToSucceedWardParams()
            {
                WardSave = wardSave,
                ArmorRules = wardRules,
                OpponentRules = opponentRules
            };

            return ChanceToWardSave(p);
        }

        private static RollStat ChanceToWardSave(ChanceToSucceedWardParams p)
        {
            var roll = GetRollStatForArmorSave(p);

            return roll;
        }


        private static RollStat GetRollStatForArmorSave(ChanceToSucceedWardParams p)
        {
            var rollstat = new RollStat()
            {
                RegHits = new Fraction(p.WardSave - 1, 6),
                UniqueSix = new Fraction(0, 1)
            };

            Utils.BoundNumerator(rollstat, 1, 6);

            return rollstat;
        }
    }
}
