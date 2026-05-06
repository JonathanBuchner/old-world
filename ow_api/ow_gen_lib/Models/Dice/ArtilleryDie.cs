using System;
using System.Collections.Generic;
using ow_gen_lib.Enums.Dice;

namespace ow_gen_lib.Models.Dice
{
    public sealed class ArtilleryDie : Die
    {
        private static readonly string[] _faces = Enum.GetNames<ArtilleryDieResult>();
        private static readonly ArtilleryDieResult[] _results =
        {
            ArtilleryDieResult.Two,
            ArtilleryDieResult.Four,
            ArtilleryDieResult.Six,
            ArtilleryDieResult.Eight,
            ArtilleryDieResult.Ten,
            ArtilleryDieResult.Misfire
        };

        public ArtilleryDie() : base(DiceType.DA)
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
