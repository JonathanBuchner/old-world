using OldWorld.Calculator.Models;
using OldWorld.Calculator.Exceptions;
using OldWorld.Domain.Enums.Equipment;
using OldWorld.Domain.Enums.SpecialRules;
using OldWorld.Calculator;
using OldWorld.Domain.Models;
using OldWorld.Domain.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OldWorld.Calculator.Versions.OldWorldV152Renegade
{
    public class Calc : ICalc
    {

        public ToWoundResult ToWound(ModelProfile a, List<string> ar, ModelProfile d, List<string> dr)
        {
            throw new NotImplementedException();
        }

        public List<Distribution> TotalBinoDistribution(ToWoundResult r, int atks)
        {
            throw new NotImplementedException();
        }
    }
}
