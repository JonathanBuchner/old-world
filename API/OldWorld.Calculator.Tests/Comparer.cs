using OldWorld.Calculator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OldWorld.Domain.Models;

namespace OldWorld.Calculator.Tests
{
    public static class CustomAssert
    {
        public static void AreEqual(RollStat expected, RollStat actual)
        {
            Assert.AreEqual(expected.RegHits.Numerator, actual.RegHits.Numerator);
            Assert.AreEqual(expected.RegHits.Denominator, actual.RegHits.Denominator);
            Assert.AreEqual(expected.UniqueSix.Numerator, actual.UniqueSix.Numerator);
            Assert.AreEqual(expected.UniqueSix.Denominator, actual.UniqueSix.Denominator);
        }
    }
}
