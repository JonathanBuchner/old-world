using System;
using System.Collections.Generic;
using System.Text;

namespace OWP_LIB.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<ArmyList> Lists { get; set; }

    }
}
