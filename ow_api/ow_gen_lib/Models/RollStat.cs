
namespace ow_gen_lib.Models
{
    public class RollStat
    {
        public Fraction RegHits { get; set; }
        public Fraction UniqueSix { get; set; }
        public List<Enums.SpecialRules.RuleName> UniqueSixEffect;
        public RollStat()
        {
            RegHits = new Fraction(0, 1);
            UniqueSix = new Fraction(0, 1);
            UniqueSixEffect = [];
        }

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
