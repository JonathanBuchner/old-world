using ow_gen_lib.Enums.EntityType;
using System;

namespace ow_gen_lib.Models
{
    public abstract class Entity
    {
        public Guid UniqueId { get; set; } = new Guid();
        public string NameId { get; set; } = String.Empty;
        public abstract EntityType EntityType { get; } 
        public string ReadableName { get; set; } = String.Empty;
    }
}
