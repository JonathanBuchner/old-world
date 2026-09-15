using OldWorld.Domain.Enums.Bases;
using OldWorld.Domain.Enums.Composition;
using OldWorld.Domain.Enums.EntityType;
using OldWorld.Domain.Enums.TroopType;
using OldWorld.Domain.Models.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace OldWorld.Domain.Models
{
    public class Model : Entity
    {
        public TroopType TroopType { get; set; }
        public TroopSubtype TroopSubtype { get; set; }
        public CompositionCategory Classification { get; set; }
        public ClassificationSub ClassificationSub { get; set; }
        public BaseSize BaseSize { get; set; }
        public List<ModelProfile> Profiles { get; set; } = [];
        public override EntityType EntityType => EntityType.Model;
        public int TotalPointsValue => Profiles.Sum(model => model.TotalPointValue);
    }
}
