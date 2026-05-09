using System.Numerics;

namespace ow_gen_lib.Models
{
    public class Fraction
    {
        public BigInteger Numerator { get; set; }

        public BigInteger Denominator { get; set; }

        public Fraction(int numerator, int denominator)
            : this(new BigInteger(numerator), new BigInteger(denominator))
        {
        }

        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public double GetValue()
        {
            return (double) Numerator / (double) Denominator;
        }

        public static Fraction Add(params Fraction[] fractions)
        {
            var result = new Fraction(0, 1);

            foreach (var fraction in fractions)
            {
                if (fraction.Denominator == BigInteger.Zero)
                    continue;

                result = new Fraction(
                    result.Numerator * fraction.Denominator + fraction.Numerator * result.Denominator,
                    result.Denominator * fraction.Denominator);
            }

            return Reduce(result);
        }

        public static Fraction Subtract(Fraction minuend, Fraction subtrahend)
        {
            var result = new Fraction(
                minuend.Numerator * subtrahend.Denominator - subtrahend.Numerator * minuend.Denominator,
                minuend.Denominator * subtrahend.Denominator);

            if (result.Numerator < 0)
                result.Numerator = 0;

            return Reduce(result);
        }

        public static Fraction Multiply(params Fraction[] fractions)
        {
            var result = new Fraction(BigInteger.One, BigInteger.One);

            foreach (var fraction in fractions)
            {
                if (fraction.Denominator == BigInteger.Zero)
                {
                    return new Fraction(0, 1);
                }

                result = new Fraction(
                    result.Numerator * fraction.Numerator,
                    result.Denominator * fraction.Denominator);
            }

            return Reduce(result);
        }

        // NOTE TO SELF... Is Euclid the most effecient?
        public static Fraction Reduce(Fraction fraction)
        {
            if (fraction.Denominator == BigInteger.Zero || fraction.Numerator == BigInteger.Zero)
                return new Fraction(0, 1);
      

            var divisor = BigInteger.GreatestCommonDivisor(BigInteger.Abs(fraction.Numerator), BigInteger.Abs(fraction.Denominator));
            var numerator = fraction.Numerator / divisor;
            var denominator = fraction.Denominator / divisor;

            if (denominator < BigInteger.Zero)
            {
                numerator *= -1;
                denominator *= -1;
            }

            return new Fraction(numerator, denominator);
        }

        public static BigInteger LeastCommonDenominator(params Fraction[] fractions)
        {
            var result = BigInteger.One;

            foreach (var fraction in fractions)
                result = BigInteger.Abs(result * fraction.Denominator) / BigInteger.GreatestCommonDivisor(result, fraction.Denominator);

            return result;
        }
    }
}
