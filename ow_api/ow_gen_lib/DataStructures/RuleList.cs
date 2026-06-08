using ow_gen_lib.CustomExceptions;
using ow_gen_lib.Data.Rules;
using ow_gen_lib.Enums.SpecialRules;
using ow_gen_lib.Models;
using EntityTypeEnum = ow_gen_lib.Enums.EntityType.EntityType;

namespace ow_gen_lib.DataStructures
{
    public class RuleList : Entry
    {
        private readonly Dictionary<RuleEffect, Rule> _rules = [];
        public override EntityTypeEnum EntityType => EntityTypeEnum.Rules;

        public void Add(params string[] rules)
        {
            foreach (var rule in rules)
            {
                var rule_enum = ParseRule(rule);
                Add(rule_enum);
            }
        }

        public void Add(params RuleEffect[] rules)
        {
            foreach (var rule in rules)
            {
                if (_rules.ContainsKey(rule))
                    throw new RuleCustomException($"Rules: Rule already exists. Rule: {rule}");

                if (!RuleData.Entries.ContainsKey(rule))
                    throw new RuleCustomException($"Rules: Rule not in data. Rule: {rule}");

                _rules.Add(rule, RuleData.Entries[rule]);
            }
        }

        public void Remove(params string[] rules)
        {
            foreach (var rule in rules)
            {
                var rule_enum = ParseRule(rule);
                Remove(rule_enum);
            }
        }

        public void Remove(params RuleEffect[] rules)
        {
            foreach (var rule in rules)
            {
                var response_isok = _rules.Remove(rule);
                if (!response_isok)
                    throw new RuleCustomException("Rules: Failure to remove entry");
            }
        }

        public List<RuleEffect> FindRelaventRules(params string[] rules)
        {
            var list = new List<RuleEffect>();

            foreach (var rule in rules)
            {
                var rule_enum = ParseRule(rule);

                if (IsRelevant(rule_enum))
                    list.Add(rule_enum);
            }

            return list;
        }

        public List<RuleEffect> FindRelaventRules(params RuleEffect[] rules)
        {
            var list = new List<RuleEffect>();

            foreach (var rule in rules)
            {
                if (IsRelevant(rule))
                    list.Add(rule);
            }

            return list;
        }

        public int CountRelaventRules(params string[] rules)
        {
            var count = 0;

            foreach (var rule in rules)
            {
                var rule_enum = ParseRule(rule);

                if (IsRelevant(rule_enum))
                    count++;
            }

            return count;
        }

        public int CountRelaventRules(params RuleEffect[] rules)
        {
            var count = 0;

            foreach (var rule in rules)
            {
                if (IsRelevant(rule))
                    count++;
            }

            return count;
        }

        public bool IsRelevant(RuleEffect rule)
        {
            if (!_rules.ContainsKey(rule))
                return false;

            return _rules[rule].Active && _rules[rule].Selected;
        }

        public bool Contains(string rule)
        {
            return _rules.ContainsKey(ParseRule(rule));
        }

        public bool Contains(RuleEffect rule) 
        {
            return _rules.ContainsKey(rule);
        }

        public IReadOnlyList<RuleEffect> GetReadOnlyList()
        {
            return _rules.Keys.ToList().AsReadOnly();
        }

        public IReadOnlySet<RuleEffect> GetReadOnlySet()
        {
            return _rules.Keys.ToHashSet();
        }

        public IReadOnlyDictionary<RuleEffect, Rule> GetReadOnlyDictionary()
        {
            return _rules;
        }

        public HashSet<RuleEffect> GetHashSetCopy()
        {
            return _rules.Keys.ToHashSet();
        }

        public List<RuleEffect> GetListCopy()
        {
            return _rules.Keys.ToList();
        }

        public Dictionary<RuleEffect, Rule> GetDictionaryCopy()
        {
            return _rules.ToDictionary(rule => rule.Key, rule => rule.Value);
        }

        public static RuleEffect ParseRule(string rule)
        {
            if (Enum.TryParse(rule, ignoreCase: true, out RuleEffect ruleName))
                return ruleName;

            throw new RuleCustomException("Rules: Invalid rule name");
        }
    }
}
