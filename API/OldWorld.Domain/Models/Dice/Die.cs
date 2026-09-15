using System;
using System.Collections.Generic;
using OldWorld.Domain.Enums.Dice;

namespace OldWorld.Domain.Models.Dice
{
    public abstract class Die
    {
        protected static readonly Random Random = Random.Shared;

        protected Die(Dice_DiceType diceType)
        {
            DiceType = diceType;
        }

        public Dice_DiceType DiceType { get; }

        public abstract IReadOnlyList<string> Faces { get; }

        public abstract int Roll();

        public abstract Enum RollEnum();
    }
}
