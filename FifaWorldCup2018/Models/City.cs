using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifaWorldCup2018.Models
{
    public class City
    {
        public int CityID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Venue Venue { get; set; }
    }
}