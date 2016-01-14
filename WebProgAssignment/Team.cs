using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgAssignment
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }

        public List<Player> Players { get; set; }

        public Team()
        {
            Players = new List<Player>();
        }
    }
}