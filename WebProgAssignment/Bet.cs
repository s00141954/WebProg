using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgAssignment
{
    public class Bet
    {
        public int BetId { get; set; }
        public int FixtureId { get; set; }
        public string BetTime { get; set; }
        public string User { get; set; }
        public string PlayerId { get; set; }
    }
}