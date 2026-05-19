using ow_gen_lib.Enums.Bases;
using ow_gen_lib.Enums.Composition;
using ow_gen_lib.Enums.EntityType;
using ow_gen_lib.Enums.TroopType;
using ow_gen_lib.Models.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace ow_gen_lib.Models
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
