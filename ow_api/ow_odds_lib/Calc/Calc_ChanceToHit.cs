
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;

using ow_gen_lib.DataStructures;
using ow_gen_lib.Enums.SpecialRules;
using ow_gen_lib.Models;

namespace ow_odds_lib.Calc
{
    public static class Calc_ChanceToHit
    {
        public class ChanceToHitParams
        {
            public int AttWs { get; set; }
            public RuleList AttRules { get; set; } = new RuleList();
            public int DefWs { get; set; }
            public RuleList DefRules { get; set; } = new RuleList();
        }

        public static RollStat ChanceToHit(int AttackerWs, RuleList AttackerRules, int DefenderWs, RuleList DefenderRules)
        {
            var p = new ChanceToHitParams()
            {
                AttWs = AttackerWs,
                AttRules = AttackerRules,
                DefWs = DefenderWs,
                DefRules = DefenderRules
            };

            // Obviously this order matters
            AddWeaponSkillAdjustments(p);               // First Adjust WS for +1 WS or +2 WS.
            var roll = GetRollStatForToHitCombat(p);    // Using WS, figure out how many faces hit (which is the enumerator)
            AddToHitAdjustments(roll, p);               // Adjust faces that hit / numerator for +1 or -1 to hit.
            OverrideToHit(roll, p);                     // Override to hit with always hit rules.
            AddRerollAdjustments(roll, p);              // Adjust numerator and denominator for rerolls.

            return roll;
        }

        private static void AddWeaponSkillAdjustments(ChanceToHitParams p)
        {
            // Change attacker WS
            var AddToAtt = p.AttRules.CountRelaventRules(
                    RuleName.Add1Attr
                );
            var SubToAtt = p.AttRules.CountRelaventRules(
                    RuleName.Subtract1Attr
                );
            p.AttWs += AddToAtt - SubToAtt;

            // Change defender WS
            var AddToDef = p.DefRules.CountRelaventRules(
                    RuleName.Add1Attr
                );
            var SubToDef= p.DefRules.CountRelaventRules(
                    RuleName.Subtract1Attr
                );
            p.DefWs += AddToDef - SubToDef;

            // FiWS cannot be below 1
            p.AttWs = Utils.Bound(p.AttWs, 1, 10);
            p.DefWs = Utils.Bound(p.DefWs, 1, 10);
        }

        private static void AddToHitAdjustments(RollStat r, ChanceToHitParams p)
        {
            var AddToAtt = p.AttRules.CountRelaventRules(
                    RuleName.Add1Result
                );
            var SubToAtt = p.DefRules.CountRelaventRules(
                    RuleName.Subtract1Result
                );

            // Don't account for unique 6 because 6's always hit
            r.RegHits.Numerator += AddToAtt - SubToAtt;
            

            // 1 Always miss. 6's always hit.
            // Will account for special sixes
            Utils.BoundNumerator(r, 1, 5);
        }

        private static void OverrideToHit(RollStat r, ChanceToHitParams p)
        {
            var alwaysHitOn2 = p.AttRules.CountRelaventRules(
                    RuleName.AlwaysHitOn2
                );
            var alwaysHitOn3 = p.AttRules.CountRelaventRules(
                    RuleName.AlwaysHitOn3
                );
            var alwaysHitOn4 = p.AttRules.CountRelaventRules(
                    RuleName.AlwaysHitOn4
                );
            var alwaysHitOn5 = p.AttRules.CountRelaventRules(
                    RuleName.AlwaysHitOn5
                );

            if (alwaysHitOn5 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 2);

            if (alwaysHitOn4 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 3);

            if (alwaysHitOn3 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 4);

            if (alwaysHitOn2 > 0)
                r.RegHits.Numerator = BigInteger.Max(r.RegHits.Numerator, 5);
        }

        private static void AddRerollAdjustments(RollStat r, ChanceToHitParams p)
        {
            // Empty rolls 
            var reroll_fails = new RollStat() { RegHits = new Fraction(0, 1), UniqueSix = new Fraction(0, 1) };
            var reroll_succs = new RollStat() { RegHits = new Fraction(0, 1), UniqueSix = new Fraction(0, 1) };

            // Rerolling failure. Max between rerolling misses and rerolling only ones
            var isRerollFails = CheckFor_RerollFailuresToHit(p);
            if (isRerollFails)
                reroll_fails = Utils.RerollFailures(r);

            if (!isRerollFails && CheckFor_Reroll1ToHit(p))
                reroll_fails = Utils.RerollOnes(r);

            // Rerolling successes. Max between rerolling successes and rerolling only sixes
            var isRerollHit = CheckFor_RerollSuccessesToHit(p);
            if (isRerollHit)
                reroll_succs = Utils.RerollSuccesses(r);

            if (!isRerollHit && CheckFor_Reroll6ToHit(p))
                reroll_succs = Utils.RerollSixes(r);

            // Add rerolling
            r.Add(reroll_fails, reroll_succs);
        }

        private static RollStat GetRollStatForToHitCombat(ChanceToHitParams p)
        {
            var roll = new RollStat()
            {
                RegHits = GetToHitBase(p),
                UniqueSix = new Fraction(0, 1)
            };

            AddSpecial6Rules(roll, p);

            return roll;
        }

        private static Fraction GetToHitBase(ChanceToHitParams p)
        {
            if (p.AttWs * 2 < p.DefWs)                  // Hitting on 5's
                return new Fraction(2, 6);

            else if (p.AttWs <= p.DefWs)                // Hitting on 4's
                return new Fraction(3, 6);

            else if (p.AttWs > 2 * p.DefWs)             // Hitting on 2's
                return new Fraction(5, 6);

            else                                        // Hitting on 3's
                return new Fraction(4, 6);
        }

        private static void AddSpecial6Rules(RollStat r, ChanceToHitParams p)
        {
            r.UniqueSixEffect = p.AttRules.FindRelaventRules(
                    RuleName.Poison
                );

            if (r.UniqueSixEffect.Count > 0)
                Utils.AddUniqueSix(r);

        }

        private static bool CheckFor_RerollSuccessesToHit(ChanceToHitParams p)
        {
            var count = p.DefRules.CountRelaventRules(
                   RuleName.RerollSuccesses
                );

            return count > 0;
        }

        private static bool CheckFor_RerollFailuresToHit(ChanceToHitParams p)
        {
            var count = p.AttRules.CountRelaventRules(
                  RuleName.RerollMisses,
                  RuleName.Hatred
               );

            return count > 0;
        }

        private static bool CheckFor_Reroll6ToHit(ChanceToHitParams p)
        {
            var count = p.DefRules.CountRelaventRules(
                   RuleName.Reroll6
                );

            return count > 0;
        }

        private static bool CheckFor_Reroll1ToHit(ChanceToHitParams p)
        {
            var count = p.AttRules.CountRelaventRules(
                   RuleName.Reroll1,
                   RuleName.PrimalFury,
                   RuleName.GrudgeRune,
                   RuleName.InnerCircle,
                   RuleName.GuardiansOfTheTemple
                );

            return count > 0;
        }
    }
}
