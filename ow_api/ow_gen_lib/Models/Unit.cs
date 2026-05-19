using ow_gen_lib.Enums.EntityType;
using System;
using System.Collections.Generic;
using System.Text;

namespace ow_gen_lib.Models
{
    public class Unit : Entity
    {
        public Model? PrimaryModel { get; set; }
        public int PrimaryModelCount { get; set; }
        public int Files { get; set;  }
        public List<Model> AdditionalModels { get; set; } = [];
        public override EntityType EntityType => EntityType.Unit;
        public int TotalPointsValue => (PrimaryModel == null ? 0 : PrimaryModel.TotalPointsValue * PrimaryModelCount) + AdditionalModels.Sum(model => model.TotalPointsValue);
    }
}
