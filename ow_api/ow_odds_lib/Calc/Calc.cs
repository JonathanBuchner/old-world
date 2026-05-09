using ow_gen_lib.CustomExceptions;
using ow_gen_lib.Enums.Equipment;
using ow_gen_lib.Enums.SpecialRules;
using ow_gen_lib.Interfaces;
using ow_gen_lib.Models;
using ow_gen_lib.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ow_odds_lib.Calc
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
