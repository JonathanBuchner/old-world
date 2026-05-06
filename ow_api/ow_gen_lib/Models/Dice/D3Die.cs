using System;
using System.Collections.Generic;
using ow_gen_lib.Enums.Dice;

namespace ow_gen_lib.Models.Dice
{
    public sealed class D3Die : Die
    {
        private static readonly string[] _faces = Enum.GetNames<D3DieResult>();

        public D3Die() : base(DiceType.D3)
        {
        }

        public override IReadOnlyList<string> Faces => _faces;

        public override int Roll()
        {
            return Random.Next(1, 4);
        }

        public override Enum RollEnum()
        {
            return (D3DieResult) Roll();
        }
    }
}
