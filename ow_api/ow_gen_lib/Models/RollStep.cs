using ow_gen_lib.Enums.GamePhases;
using System;
using System.Collections.Generic;
using System.Text;

namespace ow_gen_lib.Models
{
    public class RollStep
    {
        public RollStepEnum Step { get; set; }
        public Fraction Probability { get; set; } = new Fraction();
        public List<RollStep> Children { get; set; } = [];
    }
}
