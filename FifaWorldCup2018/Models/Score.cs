using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifaWorldCup2018.Models
{
    public class Score
    {
        public int Id { get; set; }
        public int Points { get; set; }

        public virtual Team Team { get; set; }
        public virtual Game Game { get; set; }
    }
}