using System.Numerics;
using ow_gen_lib.DataStructures;
using ow_gen_lib.Enums.SpecialRules;
using ow_gen_lib.Models;

namespace ow_odds_lib.Calc
{
    public static class Calc_ChanceToNotArmor
    {
        private class ChanceToSucceedArmorParams
        {
            public int ArmorSave { get; set; }
            public RuleList ArmorRules { get; set; } = new RuleList();
            public RuleList OpponentRules { get; set; } = new RuleList();
        }

        public static RollStat ChanceToSucceedArmor(int ArmorSave, RuleList ArmorRules, RuleList OpponentRules)
        {
            var p = new ChanceToSucceedArmorParams()
            {
                ArmorSave = ArmorSave,
                ArmorRules = ArmorRules,
                OpponentRules = OpponentRules
            };

            return ChanceToSucceedArmor(p);
        }

        private static RollStat ChanceToSucceedArmor(ChanceToSucceedArmorParams p)
        {
            var roll = GetRollStatForArmorSave(p);
            AddArmorSaveResultAdjustments(roll, p);
            OverrideArmorSave(roll, p);
            AddRerollAdjustments(roll, p);

            return roll;
        }

        private static void AddArmorSaveResultAdjustments(RollStat r, ChanceToSucceedArmorParams p)
        {
            var addToArmorSave = p.ArmorRules.CountRelaventRules(
                    RuleEffect.Add1Result
                );
            var subtractFromArmorSave = p.OpponentRules.CountRelaventRules(
                    RuleEffect.Subtract1Result
                );

            r.RegHits.Numerator += addToArmorSave - subtractFromArmorSave;

            Utils.BoundNumerator(r, 1, 6);
        }

        private static void OverrideArmorSave(RollStat r, ChanceToSucceedArmorParams p)
        {
            var alwaysArmorSaveOn2 = p.ArmorRules.CountRelaventRules(
                    RuleEffect.AlwaysArmorSaveOn2
                );
            var alwaysArmorSaveOn3 = p.ArmorRules.CountRelaventRules(
                    RuleEffect.AlwaysArmorSaveOn3
                );
            var alwaysArmorSaveOn4 = p.ArmorRules.CountRelaventRules(
                    RuleEffect.AlwaysArmorSaveOn4
                );
            var alwaysArmorSaveOn5 = p.ArmorRules.CountRelaventRules(
                    RuleEffect.AlwaysArmorSaveOn5
                );
            var alwaysArmorSaveOn6 = p.ArmorRules.CountRelaventRules(
                    RuleEffect.AlwaysArmorSaveOn6
                );

            if (alwaysArmorSaveOn6 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 1);

            if (alwaysArmorSaveOn5 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 2);

            if (alwaysArmorSaveOn4 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 3);

            if (alwaysArmorSaveOn3 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 4);

            if (alwaysArmorSaveOn2 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 5);
        }

        private static void AddRerollAdjustments(RollStat r, ChanceToSucceedArmorParams p)
        {
            var rerollFails = new RollStat() { RegHits = new Fraction(0, 1), UniqueSix = new Fraction(0, 1) };
            var rerollSuccesses = new RollStat() { RegHits = new Fraction(0, 1), UniqueSix = new Fraction(0, 1) };

            var isRerollFails = CheckFor_RerollFailuresToArmorSave(p);
            if (isRerollFails)
                rerollFails = Utils.RerollFailures(r);

            if (!isRerollFails && CheckFor_Reroll1ToArmorSave(p))
                rerollFails = Utils.RerollOnes(r);

            var isRerollSuccesses = CheckFor_RerollSuccessesToArmorSave(p);
            if (isRerollSuccesses)
                rerollSuccesses = Utils.RerollSuccesses(r);

            if (!isRerollSuccesses && CheckFor_Reroll6ToArmorSave(p))
                rerollSuccesses = Utils.RerollSixes(r);

            r.Add(rerollFails, rerollSuccesses);
        }

        private static RollStat GetRollStatForArmorSave(ChanceToSucceedArmorParams p)
        {
            var rollstat = new RollStat()
            {
                RegHits = new Fraction(p.ArmorSave - 1, 6),
                UniqueSix = new Fraction(0, 1)
            };

            Utils.BoundNumerator(rollstat, 1, 6);

            return rollstat;
        }

        private static bool CheckFor_RerollSuccessesToArmorSave(ChanceToSucceedArmorParams p)
        {
            var count = p.OpponentRules.CountRelaventRules(
                    RuleEffect.RerollSuccesses
                );

            return count > 0;
        }

        private static bool CheckFor_RerollFailuresToArmorSave(ChanceToSucceedArmorParams p)
        {
            var count = p.ArmorRules.CountRelaventRules(
                    RuleEffect.RerollMisses
                );

            return count > 0;
        }

        private static bool CheckFor_Reroll6ToArmorSave(ChanceToSucceedArmorParams p)
        {
            var count = p.OpponentRules.CountRelaventRules(
                    RuleEffect.Reroll6
                );

            return count > 0;
        }

        private static bool CheckFor_Reroll1ToArmorSave(ChanceToSucceedArmorParams p)
        {
            var count = p.ArmorRules.CountRelaventRules(
                    RuleEffect.Reroll1
                );

            return count > 0;
        }
    }
}
