
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

using ow_gen_lib.CustomExceptions;
using ow_gen_lib.Models;

namespace ow_odds_lib.Calc
{
    public static class Utils
    {
        public static int Bound(int s, int floor, int ceil)
        {
            if (s < floor)
                return floor;

            if (s > ceil)
                return ceil;

            return s;
        }

        public static void BoundNumerator(Fraction f, int floor, int ceil)
        {
            if (f.Numerator < floor)
                f.Numerator = floor;

            if (f.Numerator > ceil)
                f.Numerator = ceil;
        }

        public static void BoundNumerator(RollStat r, int floor, int ceil)
        {
            var total_hit = r.RegHits.Numerator + r.UniqueSix.Numerator;

            if (total_hit < floor)
                r.RegHits.Numerator = floor;

            if (total_hit > ceil)
                r.RegHits.Numerator = ceil - r.UniqueSix.Numerator;
        }

        public static void AddUniqueSix(RollStat r)
        {
            r.UniqueSix = new Fraction(1, 6);
            r.RegHits.Numerator -= 1;

            if (r.RegHits.Numerator < 0)
                throw new SixAlwaysHit("AddUniqueSix. Regular hits should not be negative.");
        }

        public static RollStat RerollFailures(RollStat r)
        {
            var totalHits = Fraction.Add(r.RegHits, r.UniqueSix);
            var misses = new Fraction(totalHits.Denominator - totalHits.Numerator, totalHits.Denominator);

            return new RollStat()
            {
                RegHits = Fraction.Multiply(misses, r.RegHits),
                UniqueSix = Fraction.Multiply(misses, r.UniqueSix)
            };
        }

        public static RollStat RerollOnes(RollStat r)
        {
            var one = new Fraction(1, 6);

            return new RollStat()
            {
                RegHits = Fraction.Multiply(one, r.RegHits),
                UniqueSix = Fraction.Multiply(one, r.UniqueSix)
            };
        }

        public static RollStat RerollSuccesses(RollStat r)
        {
            var totalHits = Fraction.Add(r.RegHits, r.UniqueSix);

            return new RollStat()
            {
                RegHits = Fraction.Add(Fraction.Multiply(totalHits, r.RegHits), new Fraction(-r.RegHits.Numerator, r.RegHits.Denominator)),
                UniqueSix = Fraction.Add(Fraction.Multiply(totalHits, r.UniqueSix), new Fraction(-r.UniqueSix.Numerator, r.UniqueSix.Denominator))
            };
        }

        public static RollStat RerollSixes(RollStat r)
        {
            /**
             * Rerolling 6s means the original natural-6 result no longer counts immediately.
             * First, save the original hit profile so the rerolled 6 can use the same normal
             * chance to become a regular hit or poison hit. Then remove the original 1/6
             * natural-6 result from whichever bucket owns it: poison if poison is active,
             * otherwise regular hits. Finally, add back the expected result of rerolling
             * that 1/6 die face by multiplying 1/6 by the original hit chances.
             */
            var originalRegHits = new Fraction(r.RegHits.Numerator, r.RegHits.Denominator);
            var originalPoisonHits = new Fraction(r.UniqueSix.Numerator, r.UniqueSix.Denominator);
            var six = new Fraction(1, 6);

            var result = new RollStat()
            {
                RegHits = Fraction.Multiply(six, originalRegHits),
                UniqueSix = Fraction.Multiply(six, originalPoisonHits)
            };

            if (r.UniqueSix.Numerator > 0)
            {
                result.UniqueSix = Fraction.Add(result.UniqueSix, new Fraction(-1, 6));
            }
            else
            {
                result.RegHits = Fraction.Add(result.RegHits, new Fraction(-1, 6));
            }

            return result;
        }
    }
}
