
namespace ow_gen_lib.Models
{
    public class RollStat
    {
        public Fraction RegHits { get; set; } = new Fraction(0, 1);
        public Fraction UniqueSix { get; set; } = new Fraction(0, 1);
        public Fraction UniqueFive { get; set; } = new Fraction(0, 1);
        public List<Enums.SpecialRules.RuleName> UniqueSixEffect = [];
        public List<Enums.SpecialRules.RuleName> UniqueFiveEffect = [];

        public void Reduce()
        {
            RegHits = Fraction.Reduce(RegHits);
            UniqueSix = Fraction.Reduce(UniqueSix);
        }

        public void Add(params RollStat[] rollStats)
        {
            foreach (var rollStat in rollStats)
            {
                RegHits = Fraction.Add(RegHits, rollStat.RegHits);
                UniqueSix = Fraction.Add(UniqueSix, rollStat.UniqueSix);
            }
        }

    }
}
