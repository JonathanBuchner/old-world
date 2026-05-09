using System.Collections.Generic;
using System.Reflection;
using ow_gen_lib.DataStructures;
using ow_gen_lib.Enums.SpecialRules;
using ow_gen_lib.Models;
using ow_odds_lib.Calc;

namespace ow_odds_lib_tests
{
    [TestClass]
    public class ChanceToHit_tests
    {
        public static IEnumerable<object[]> cases_basic()
        {
            yield return Case("Basic_1_3", 1, [], 3, [], 1, 3, 0, 1);
            yield return Case("Basic_1_1", 1, [], 1, [], 1, 2, 0, 1);
            yield return Case("Basic_3_1", 3, [], 1, [], 5, 6, 0, 1);

            yield return Case("Basic_3_7", 3, [], 7, [], 1, 3, 0, 1);
            yield return Case("Basic_4_7", 4, [], 7, [], 1, 2, 0, 1);
            yield return Case("Basic_7_7", 7, [], 7, [], 1, 2, 0, 1);
            yield return Case("Basic_8_7", 8, [], 7, [], 2, 3, 0, 1);
            yield return Case("Basic_5_2", 5, [], 2, [], 5, 6, 0, 1);
        }

        public static IEnumerable<object[]> cases_poison()
        {
            yield return Case("Poison_1_3", 1, [RuleName.Poison], 3, [], 1, 6, 1, 6);
            yield return Case("Poison_1_1", 1, [RuleName.Poison], 1, [], 1, 3, 1, 6);
            yield return Case("Poison_3_1", 3, [RuleName.Poison], 1, [], 2, 3, 1, 6);
        }

        public static IEnumerable<object[]> cases_reroll_misses()
        {
            yield return Case("RerollMiss_1_3", 1, [RuleName.RerollMisses], 3, [], 5, 9, 0, 1);
            yield return Case("RerollMiss_1_1", 1, [RuleName.RerollMisses], 1, [], 3, 4, 0, 1);
            yield return Case("RerollMiss_3_1", 3, [RuleName.RerollMisses], 1, [], 35, 36, 0, 1);
        }

        public static IEnumerable<object[]> cases_reroll_hits()
        {
            yield return Case("RerollHits_1_3", 1, [], 3, [RuleName.RerollSuccesses], 1, 9, 0, 1);
            yield return Case("RerollHits_1_1", 1, [], 1, [RuleName.RerollSuccesses], 1, 4, 0, 1);
            yield return Case("RerollHits_3_1", 3, [], 1, [RuleName.RerollSuccesses], 25, 36, 0, 1);
        }

        public static IEnumerable<object[]> cases_reroll_ones()
        {
            yield return Case("Reroll1s_1_3", 1, [RuleName.Reroll1], 3, [], 7, 18, 0, 1);
            yield return Case("Reroll1s_1_1", 1, [RuleName.Reroll1], 1, [], 7, 12, 0, 1);
            yield return Case("Reroll1s_3_1", 3, [RuleName.Reroll1], 1, [], 35, 36, 0, 1);
        }

        public static IEnumerable<object[]> cases_reroll_sixes()
        {
            yield return Case("Reroll6s_1_3", 1, [], 3, [RuleName.Reroll6], 2, 9, 0, 1);
            yield return Case("Reroll6s_1_1", 1, [], 1, [RuleName.Reroll6], 5, 12, 0, 1);
            yield return Case("Reroll6s_3_1", 3, [], 1, [RuleName.Reroll6], 29, 36, 0, 1);
        }

        public static IEnumerable<object[]> cases_attacker_weapon_skill_adjustments()
        {
            yield return Case("+1_WS_Attacker_1_3", 1, [RuleName.Add1Attr], 3, [], 1, 2, 0, 1);
            yield return Case("+1_WS_Attacker_1_1", 1, [RuleName.Add1Attr], 1, [], 2, 3, 0, 1);
            yield return Case("+1_WS_Attacker_3_1", 3, [RuleName.Add1Attr], 1, [], 5, 6, 0, 1);

            yield return Case("-1_WS_Attacker_1_3", 1, [RuleName.Subtract1Attr], 3, [], 1, 3, 0, 1);
            yield return Case("-1_WS_Attacker_1_1", 1, [RuleName.Subtract1Attr], 1, [], 1, 2, 0, 1);
            yield return Case("-1_WS_Attacker_3_1", 3, [RuleName.Subtract1Attr], 1, [], 2, 3, 0, 1);
        }

        public static IEnumerable<object[]> cases_defender_weapon_skill_adjustments()
        {
            yield return Case("+1_WS_Defender_1_3", 1, [], 3, [RuleName.Add1Attr], 1, 3, 0, 1);
            yield return Case("+1_WS_Defender_1_1", 1, [], 1, [RuleName.Add1Attr], 1, 2, 0, 1);
            yield return Case("+1_WS_Defender_2_2", 2, [], 2, [RuleName.Add1Attr], 1, 2, 0, 1);
            yield return Case("+1_WS_Defender_3_1", 3, [], 1, [RuleName.Add1Attr], 2, 3, 0, 1);

            yield return Case("-1_WS_Defender_1_3", 1, [], 3, [RuleName.Subtract1Attr], 1, 2, 0, 1);
            yield return Case("-1_WS_Defender_1_1", 1, [], 1, [RuleName.Subtract1Attr], 1, 2, 0, 1);
            yield return Case("-1_WS_Defender_2_2", 2, [], 2, [RuleName.Subtract1Attr], 2, 3, 0, 1);
            yield return Case("-1_WS_Defender_3_1", 3, [], 1, [RuleName.Subtract1Attr], 5, 6, 0, 1);
        }

        public static IEnumerable<object[]> cases_to_hit_result_adjustments()
        {
            yield return Case("+1_to_hit_1_3", 1, [RuleName.Add1Result], 3, [], 1, 2, 0, 1);
            yield return Case("+1_to_hit_1_1", 1, [RuleName.Add1Result], 1, [], 2, 3, 0, 1);
            yield return Case("+1_to_hit_3_1", 3, [RuleName.Add1Result], 1, [], 5, 6, 0, 1);

            yield return Case("-1_to_hit_1_3", 1, [], 3, [RuleName.Subtract1Result], 1, 6, 0, 1);
            yield return Case("-1_to_hit_1_1", 1, [], 1, [RuleName.Subtract1Result], 1, 3, 0, 1);
            yield return Case("-1_to_hit_3_1", 3, [], 1, [RuleName.Subtract1Result], 2, 3, 0, 1);
        }

        public static IEnumerable<object[]> cases_always_hit()
        {
            yield return Case("Alwayshit2_1_3", 1, [RuleName.AlwaysHitOn2], 3, [], 5, 6, 0, 1);
            yield return Case("Alwayshit3_1_1", 1, [RuleName.AlwaysHitOn3], 1, [], 2, 3, 0, 1);
            yield return Case("Alwayshit4_3_1", 3, [RuleName.AlwaysHitOn4], 1, [], 5, 6, 0, 1);
            yield return Case("Alwayshit5_3_1", 3, [RuleName.AlwaysHitOn5], 1, [], 5, 6, 0, 1);
        }

        [TestMethod]
        [DynamicData(nameof(cases_basic), DynamicDataDisplayName = nameof(GetTestName))]
        public void TEST_ChanceToHit_basic(string testName, int attackerWs, RuleList attackerRules, int defenderWs, RuleList defenderRules, RollStat expected)
        {
            AssertChanceToHit(attackerWs, attackerRules, defenderWs, defenderRules, expected);
        }

        [TestMethod]
        [DynamicData(nameof(cases_poison), DynamicDataDisplayName = nameof(GetTestName))]
        public void TEST_ChanceToHit_poison(string testName, int attackerWs, RuleList attackerRules, int defenderWs, RuleList defenderRules, RollStat expected)
        {
            AssertChanceToHit(attackerWs, attackerRules, defenderWs, defenderRules, expected);
        }

        [TestMethod]
        [DynamicData(nameof(cases_reroll_misses), DynamicDataDisplayName = nameof(GetTestName))]
        public void TEST_ChanceToHit_reroll_misses(string testName, int attackerWs, RuleList attackerRules, int defenderWs, RuleList defenderRules, RollStat expected)
        {
            AssertChanceToHit(attackerWs, attackerRules, defenderWs, defenderRules, expected);
        }

        [TestMethod]
        [DynamicData(nameof(cases_reroll_hits), DynamicDataDisplayName = nameof(GetTestName))]
        public void TEST_ChanceToHit_reroll_hits(string testName, int attackerWs, RuleList attackerRules, int defenderWs, RuleList defenderRules, RollStat expected)
        {
            AssertChanceToHit(attackerWs, attackerRules, defenderWs, defenderRules, expected);
        }

        [TestMethod]
        [DynamicData(nameof(cases_reroll_ones), DynamicDataDisplayName = nameof(GetTestName))]
        public void TEST_ChanceToHit_reroll_ones(string testName, int attackerWs, RuleList attackerRules, int defenderWs, RuleList defenderRules, RollStat expected)
        {
            AssertChanceToHit(attackerWs, attackerRules, defenderWs, defenderRules, expected);
        }

        [TestMethod]
        [DynamicData(nameof(cases_reroll_sixes), DynamicDataDisplayName = nameof(GetTestName))]
        public void TEST_ChanceToHit_reroll_sixes(string testName, int attackerWs, RuleList attackerRules, int defenderWs, RuleList defenderRules, RollStat expected)
        {
            AssertChanceToHit(attackerWs, attackerRules, defenderWs, defenderRules, expected);
        }

        [TestMethod]
        [DynamicData(nameof(cases_attacker_weapon_skill_adjustments), DynamicDataDisplayName = nameof(GetTestName))]
        public void TEST_ChanceToHit_attacker_weapon_skill_adjustments(string testName, int attackerWs, RuleList attackerRules, int defenderWs, RuleList defenderRules, RollStat expected)
        {
            AssertChanceToHit(attackerWs, attackerRules, defenderWs, defenderRules, expected);
        }

        [TestMethod]
        [DynamicData(nameof(cases_defender_weapon_skill_adjustments), DynamicDataDisplayName = nameof(GetTestName))]
        public void TEST_ChanceToHit_defender_weapon_skill_adjustments(string testName, int attackerWs, RuleList attackerRules, int defenderWs, RuleList defenderRules, RollStat expected)
        {
            AssertChanceToHit(attackerWs, attackerRules, defenderWs, defenderRules, expected);
        }

        [TestMethod]
        [DynamicData(nameof(cases_to_hit_result_adjustments), DynamicDataDisplayName = nameof(GetTestName))]
        public void TEST_ChanceToHit_to_hit_result_adjustments(string testName, int attackerWs, RuleList attackerRules, int defenderWs, RuleList defenderRules, RollStat expected)
        {
            AssertChanceToHit(attackerWs, attackerRules, defenderWs, defenderRules, expected);
        }

        [TestMethod]
        [DynamicData(nameof(cases_always_hit), DynamicDataDisplayName = nameof(GetTestName))]
        public void TEST_ChanceToHit_always_hit(string testName, int attackerWs, RuleList attackerRules, int defenderWs, RuleList defenderRules, RollStat expected)
        {
            AssertChanceToHit(attackerWs, attackerRules, defenderWs, defenderRules, expected);
        }

        private static void AssertChanceToHit(int attackerWs, RuleList attackerRules, int defenderWs, RuleList defenderRules, RollStat expected)
        {
            var actual = Calc_ChanceToHit.ChanceToHit(attackerWs, attackerRules, defenderWs, defenderRules);
            actual.Reduce();

            CustomAssert.AreEqual(expected, actual);
        }

        public static string GetTestName(MethodInfo methodInfo, object[] data)
        {
            return data[0].ToString()!;
        }

        private static object[] Case(string testName, int attackerWs, RuleName[] attackerRules, int defenderWs, RuleName[] defenderRules, int regHitsNumerator, int regHitsDenominator, int uniqueSixNumerator, int uniqueSixDenominator)
        {
            return
            [
                testName,
                attackerWs,
                Rules(attackerRules),
                defenderWs,
                Rules(defenderRules),
                new RollStat()
                {
                    RegHits = new Fraction(regHitsNumerator, regHitsDenominator),
                    UniqueSix = new Fraction(uniqueSixNumerator, uniqueSixDenominator)
                }
            ];
        }

        private static RuleList Rules(params RuleName[] rules)
        {
            var ruleList = new RuleList();
            ruleList.Add(rules);

            foreach (var rule in rules)
            {
                ruleList.GetReadOnlyDictionary()[rule].Active = true;
                ruleList.GetReadOnlyDictionary()[rule].Selected = true;
            }

            return ruleList;
        }
    }
}
