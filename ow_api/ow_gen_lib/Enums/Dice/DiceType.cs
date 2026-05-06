using System;
using System.Collections.Generic;
using System.Text;

namespace ow_gen_lib.Enums.Dice
{
    public enum DiceType
    {
        D6,       // Standard six-sided die
        D3,       // Represents results 1-3 with equal probability
        DA,       // Artillery die: 2, 4, 6, 8, 10, or Misfire
        DS        // Scatter die: four arrows and two hit symbols
    }
}
