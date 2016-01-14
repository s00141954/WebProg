using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgAssignment
{
    public class Fixture
    {
        public int FixtureId { get; set; }
        public int? GameWeek { get; set; }
        public int HTeamID { get; set; }
        public int ATeamID { get; set; }
    }
}