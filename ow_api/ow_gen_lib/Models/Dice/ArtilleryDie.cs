using System;
using System.Collections.Generic;
using ow_gen_lib.Enums.Dice;

namespace ow_gen_lib.Models.Dice
{
    public sealed class ArtilleryDie : Die
    {
        private static readonly string[] _faces = Enum.GetNames<Dice_ArtilleryDieResult>();
        private static readonly Dice_ArtilleryDieResult[] _results =
        {
            Dice_ArtilleryDieResult.Two,
            Dice_ArtilleryDieResult.Four,
            Dice_ArtilleryDieResult.Six,
            Dice_ArtilleryDieResult.Eight,
            Dice_ArtilleryDieResult.Ten,
            Dice_ArtilleryDieResult.Misfire
        };

        public ArtilleryDie() : base(Dice_DiceType.DA)
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
