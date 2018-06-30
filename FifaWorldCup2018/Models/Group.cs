using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifaWorldCup2018.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
        public ICollection<Team> Team { get; set; }
    }
}