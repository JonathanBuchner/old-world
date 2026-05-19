using System.Numerics;
using ow_gen_lib.DataStructures;
using ow_gen_lib.Enums.SpecialRules;
using ow_gen_lib.Models;

namespace ow_odds_lib.Calc
{
    public static class Calc_ChanceToWound
    {
        private class ChanceToWoundParams
        {
            public int AttS { get; set; }
            public RuleList AttR { get; set; } = new RuleList();
            public int DefT { get; set; }
            public RuleList DefR { get; set; } = new RuleList();
        }

        public static RollStat ChanceToWound(int AttackerS, RuleList AttackerRules, int DefenderT, RuleList DefenderRules)
        {
            var p = new ChanceToWoundParams()
            {
                AttS = AttackerS,
                AttR = AttackerRules,
                DefT = DefenderT,
                DefR = DefenderRules
            };

            return ChanceToWound(p);
        }

        private static RollStat ChanceToWound(ChanceToWoundParams p)
        {
            AddStrengthAndToughnessAdjustments(p);
            var roll = GetRollStatForToWound(p);
            AddToWoundAdjustments(roll, p);
            OverrideToWound(roll, p);
            AddRerollAdjustments(roll, p);

            return roll;
        }

        private static void AddStrengthAndToughnessAdjustments(ChanceToWoundParams p)
        {
            var addToAttackerWS = p.AttR.CountRelaventRules(
                    RuleName.Add1Attr
                );
            var subtractFromAttackerWs = p.AttR.CountRelaventRules(
                    RuleName.Subtract1Attr
                );

            var addToDefenderWs = p.DefR.CountRelaventRules(
                    RuleName.Add1Attr
                );
            var subtractFromDefenderWs = p.DefR.CountRelaventRules(
                    RuleName.Subtract1Attr
                );

            p.AttS += addToAttackerWS - subtractFromAttackerWs;
            p.DefT += addToDefenderWs - subtractFromDefenderWs;

            p.AttS = Utils.Bound(p.AttS, 1, 10);
            p.DefT = Utils.Bound(p.DefT, 1, 10);
        }

        private static void AddToWoundAdjustments(RollStat r, ChanceToWoundParams p)
        {
            var addToAtt = p.AttR.CountRelaventRules(
                    RuleName.Add1Result
                );
            var subToAtt = p.DefR.CountRelaventRules(
                    RuleName.Subtract1Result
                );

            r.RegHits.Numerator += addToAtt - subToAtt;

            Utils.BoundNumerator(r, 1, 5);
        }

        private static void OverrideToWound(RollStat r, ChanceToWoundParams p)
        {
            var alwaysWoundOn2 = p.AttR.CountRelaventRules(
                    RuleName.AlwaysWoundOn2
                );
            var alwaysWoundOn3 = p.AttR.CountRelaventRules(
                    RuleName.AlwaysWoundOn3
                );
            var alwaysWoundOn4 = p.AttR.CountRelaventRules(
                    RuleName.AlwaysWoundOn4
                );
            var alwaysWoundOn5 = p.AttR.CountRelaventRules(
                    RuleName.AlwaysWoundOn5
                );

            if (alwaysWoundOn5 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 2);

            if (alwaysWoundOn4 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 3);
            
            if (alwaysWoundOn3 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 4);
            
            if (alwaysWoundOn2 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 5);
        }

        private static void AddRerollAdjustments(RollStat r, ChanceToWoundParams p)
        {
            var rerollFails = new RollStat() { RegHits = new Fraction(0, 1), UniqueSix = new Fraction(0, 1) };
            var rerollSuccesses = new RollStat() { RegHits = new Fraction(0, 1), UniqueSix = new Fraction(0, 1) };

            var isRerollFails = CheckFor_RerollFailuresToWound(p);
            if (isRerollFails)
                rerollFails = Utils.RerollFailures(r);

            if (!isRerollFails && CheckFor_Reroll1ToWound(p))
                rerollFails = Utils.RerollOnes(r);

            var isRerollSuccesses = CheckFor_RerollSuccessesToWound(p);
            if (isRerollSuccesses)
                rerollSuccesses = Utils.RerollSuccesses(r);

            if (!isRerollSuccesses && CheckFor_Reroll6ToWound(p))
                rerollSuccesses = Utils.RerollSixes(r);

            r.Add(rerollFails, rerollSuccesses);
        }

        private static RollStat GetRollStatForToWound(ChanceToWoundParams p)
        {
            var roll = new RollStat()
            {
                RegHits = GetToWoundBase(p),
                UniqueSix = new Fraction(0, 1)
            };

            AddSpecial6Rules(roll, p);

            return roll;
        }

        private static Fraction GetToWoundBase(ChanceToWoundParams p)
        {
            var numerator = 3 + p.AttS - p.DefT;
            var roll = new Fraction(numerator, 6);
            Utils.BoundNumerator(roll, 1, 5);

            return roll;
        }

        private static void AddSpecial6Rules(RollStat r, ChanceToWoundParams p)
        {
            r.UniqueSixEffect = p.AttR.FindRelaventRules(
                    RuleName.CleavingBlow,
                    RuleName.KillingBlow,
                    RuleName.MonsterSlayer,
                    RuleName.ArmorBane1,
                    RuleName.ArmorBane2,
                    RuleName.ArmorBane3,
                    RuleName.ArmorBane4,
                    RuleName.ArmorBane5
                );

            if (r.UniqueSixEffect.Count > 0)
                Utils.AddUniqueSix(r);

        }

        private static bool CheckFor_RerollSuccessesToWound(ChanceToWoundParams p)
        {
            var count = p.DefR.CountRelaventRules(
                    RuleName.RerollSuccesses
                );

            return count > 0;
        }

        private static bool CheckFor_RerollFailuresToWound(ChanceToWoundParams p)
        {
            var count = p.AttR.CountRelaventRules(
                    RuleName.RerollMisses
                );

            return count > 0;
        }

        private static bool CheckFor_Reroll6ToWound(ChanceToWoundParams p)
        {
            var count = p.DefR.CountRelaventRules(
                    RuleName.Reroll6
                );

            return count > 0;
        }

        private static bool CheckFor_Reroll1ToWound(ChanceToWoundParams p)
        {
            var count = p.AttR.CountRelaventRules(
                    RuleName.Reroll1
                );

            return count > 0;
        }
    }
}
