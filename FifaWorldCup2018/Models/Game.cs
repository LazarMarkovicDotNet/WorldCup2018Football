using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FifaWorldCup2018.Models
{
    public class Game
    {
        public int GameID { get; set; }

        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }

        public virtual Group Group { get; set; }

        public virtual ICollection<Score> Scores { get; set; }


        public Team GetWinningTeam()
        {
            Team winningTeam = null;
            if (AwayGoals > HomeGoals)
            {
                // gosti pobedili
                winningTeam = AwayTeam;

            }
            else if (HomeGoals > AwayGoals)
            {
                // domaci pobedili
                winningTeam = HomeTeam;
            }
            return winningTeam;
        }

    }
}