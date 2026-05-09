using System;
using System.Collections.Generic;
using ow_gen_lib.Enums.Dice;

namespace ow_gen_lib.Models.Dice
{
    public sealed class ScatterDie : Die
    {
        private static readonly string[] _faces = Enum.GetNames<Dice_ScatterDieResult>();

        private static readonly Dice_ScatterDieResult[] _results =
        {
            Dice_ScatterDieResult.Arrow,
            Dice_ScatterDieResult.Arrow,
            Dice_ScatterDieResult.Arrow,
            Dice_ScatterDieResult.Arrow,
            Dice_ScatterDieResult.Hit,
            Dice_ScatterDieResult.Hit
        };

        public ScatterDie() : base(Dice_DiceType.DS)
        {
        }

        public override IReadOnlyList<string> Faces => _faces;

        public override int Roll()
        {
            return (int)_results[Random.Next(0, _results.Length)];
        }

        public override Enum RollEnum()
        {
            return _results[Random.Next(0, _results.Length)];
        }
    }
}
