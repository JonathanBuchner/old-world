using OldWorld.Domain.Enums.GamePhases;
using System;
using System.Collections.Generic;
using System.Text;

namespace OldWorld.Calculator.Models
{
    public class RollStep
    {
        public RollStepEnum Step { get; set; }
        public Fraction Probability { get; set; } = new Fraction();
        public List<RollStep> Children { get; set; } = [];
    }
}
