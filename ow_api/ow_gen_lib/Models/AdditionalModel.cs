using ow_gen_lib.Enums.EntityType;
using ow_gen_lib.Enums.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace ow_gen_lib.Models
{
    public class AdditionalModel : Entity
    {
        public Position Position { get; set; } = Position.Unknown;
        public int Index { get; set; } = -1;
        public override EntityType EntityType => EntityType.AdditionalModel;
        public Model? Model { get; set; }
    }
}
