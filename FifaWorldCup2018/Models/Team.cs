using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifaWorldCup2018.Models
{
    public class Team
    { 
        public int TeamID { get; set; }
        public string Name { get; set; }
        public int Titles { get; set; }
        public string Image { get; set; }
        public Group Group { get; set; }
        public ICollection<Venue> Venues { get; set; }
    }
}