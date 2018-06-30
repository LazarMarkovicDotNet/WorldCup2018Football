using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FifaWorldCup2018.Models
{
    public class Venue
    {
        public int VenueID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public DateTime BuiltYear { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}