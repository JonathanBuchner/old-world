using OldWorld.Domain.Enums.EntityType;
using OldWorld.Domain.Enums.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace OldWorld.Domain.Models
{
    public class AdditionalModel : Entity
    {
        public Position Position { get; set; } = Position.Unknown;
        public int Index { get; set; } = -1;
        public override EntityType EntityType => EntityType.AdditionalModel;
        public Model? Model { get; set; }
    }
}
