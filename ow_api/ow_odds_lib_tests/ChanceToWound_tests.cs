using System.Collections.Generic;
using System.Reflection;
using ow_gen_lib.DataStructures;
using ow_gen_lib.Enums.SpecialRules;
using ow_gen_lib.Models;
using ow_odds_lib.Calc;

namespace ow_odds_lib_tests
{
    [TestClass]
    public class ChanceToWound_tests
    {
        public static IEnumerable<object[]> cases()
        {
            yield return new object[]
            {
                "Basic_2_4",
                2,
                Rules(),
                4,
                Rules(),
                new RollStat() { RegHits = new Fraction(1, 6), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "Basic_3_4",
                3,
                Rules(),
                4,
                Rules(),
                new RollStat() { RegHits = new Fraction(1, 3), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "Basic_4_4",
                4,
                Rules(),
                4,
                Rules(),
                new RollStat() { RegHits = new Fraction(1, 2), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "Basic_5_4",
                5,
                Rules(),
                4,
                Rules(),
                new RollStat() { RegHits = new Fraction(2, 3), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "Basic_8_4",
                8,
                Rules(),
                4,
                Rules(),
                new RollStat() { RegHits = new Fraction(5, 6), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "Add1Attr_4_4",
                4,
                Rules(RuleName.Add1Attr),
                4,
                Rules(),
                new RollStat() { RegHits = new Fraction(2, 3), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "Subtract1Attr_4_4",
                4,
                Rules(),
                4,
                Rules(RuleName.Subtract1Attr),
                new RollStat() { RegHits = new Fraction(1, 3), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "Add1Result_4_4",
                4,
                Rules(RuleName.Add1Result),
                4,
                Rules(),
                new RollStat() { RegHits = new Fraction(2, 3), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "Subtract1Result_4_4",
                4,
                Rules(),
                4,
                Rules(RuleName.Subtract1Result),
                new RollStat() { RegHits = new Fraction(1, 3), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "RerollMisses_4_4",
                4,
                Rules(RuleName.RerollMisses),
                4,
                Rules(),
                new RollStat() { RegHits = new Fraction(3, 4), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "Reroll1_4_4",
                4,
                Rules(RuleName.Reroll1),
                4,
                Rules(),
                new RollStat() { RegHits = new Fraction(7, 12), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "RerollSuccesses_4_4",
                4,
                Rules(),
                4,
                Rules(RuleName.RerollSuccesses),
                new RollStat() { RegHits = new Fraction(1, 4), UniqueSix = new Fraction(0, 1) }
            };

            yield return new object[]
            {
                "Reroll6_4_4",
                4,
                Rules(),
                4,
                Rules(RuleName.Reroll6),
                new RollStat() { RegHits = new Fraction(5, 12), UniqueSix = new Fraction(0, 1) }
            };
        }

        [TestMethod]
        [DynamicData(nameof(cases), DynamicDataDisplayName = nameof(GetTestName))]
        public void TEST_ChanceToWound(string testName, int attackerS, RuleList attackerRules, int defenderT, RuleList defenderRules, RollStat expected)
        {
            var actual = Calc_ChanceToWound.ChanceToWound(attackerS, attackerRules, defenderT, defenderRules);
            actual.Reduce();

            CustomAssert.AreEqual(expected, actual);
        }

        public static string GetTestName(MethodInfo methodInfo, object[] data)
        {
            return data[0].ToString()!;
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
