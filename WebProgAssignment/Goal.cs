using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProgAssignment
{
    public class Goal
    {
        public int GoalId { get; set; }
        public int FixtureId { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
    }
}