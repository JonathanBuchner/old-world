using ow_gen_lib.CustomExceptions;
using System.Reflection.Metadata.Ecma335;

namespace ow_gen_lib.Models
{
    public class ToWoundResult
    {
        public RollStat? Hit { get; set; }
        public RollStat? Wound { get; set; }
        public RollStat? Armor { get; set; }
        public RollStat? Ward { get; set; }
        public RollStat? Regen { get; set; }
        public Fraction Wounds
        {
            get => CalculateWounds();
        }
        public Fraction CombatRes
        {
            get => CalculateCombatRes();
        }
        
        private Fraction CalculateCombatRes()
        {
            Validate();
            throw new NotImplementedException();
        }

        private Fraction CalculateWounds()
        {
            Validate();
            throw new NotImplementedException();
        }

        private void Validate()
        {
            if (Hit == null || Hit.RegHits.Denominator == 0)
                throw new InvalidToWoundResultCustomException("Invalid hit fraction");

            if (Wound == null || Wound.RegHits.Denominator == 0)
                throw new InvalidToWoundResultCustomException("Invalid wound fraction");

            if (Armor == null || Armor.RegHits.Denominator == 0)
                throw new InvalidToWoundResultCustomException("Invalid armor fraction");

            if (Ward == null || Ward.RegHits.Denominator == 0)
                throw new InvalidToWoundResultCustomException("Invalid ward fraction");

            if (Regen == null || Regen.RegHits.Denominator == 0)
                throw new InvalidToWoundResultCustomException("Invalid regen fraction");
        }
    }
}


