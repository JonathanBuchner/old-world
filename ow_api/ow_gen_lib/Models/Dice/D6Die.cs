using System;
using System.Collections.Generic;
using ow_gen_lib.Enums.Dice;

namespace ow_gen_lib.Models.Dice
{
    public sealed class D6Die : Die
    {
        private static readonly string[] _faces = Enum.GetNames<D6DieResult>();

        public D6Die() : base(DiceType.D6)
        {
        }

        public override IReadOnlyList<string> Faces => _faces;

        public override int Roll()
        {
            return Random.Next(1, 7);
        }

        public override Enum RollEnum()
        {
            return (D6DieResult)Roll();
        }
    }
}
