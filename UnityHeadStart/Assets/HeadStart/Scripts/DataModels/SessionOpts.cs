using System;
using Assets.HeadStart.Features.Database;
using HeadStart;

namespace Assets.HeadStart.Core
{
    public class SessionOpts
    {
        public PlayerSettings PlayerSettings;
        public User User;
        public int Points;
        public int ToiletPaper;
        public bool IsChallenge;

        public Score GetScore()
        {
            League league = League.GetThisWeeksLeague();
            return new Score()
            {
                UserLocalId = User.LocalId,
                Points = Points,
                Week = league.Week,
                Year = league.Year
            };
        }

        public override string ToString()
        {
            return "{" + String.Format(@"
                Points: {0},
                ToiletPaper: {1}
            ", Points, ToiletPaper) + "}";
        }
    }
}