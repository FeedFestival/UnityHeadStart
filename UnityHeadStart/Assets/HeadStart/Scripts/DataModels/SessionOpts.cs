using System;
using Assets.HeadStart.Features.Database;
using Assets.Scripts.utils;

namespace Assets.HeadStart.Core
{
    public class SessionOpts
    {
        public User User;
        public int Points;
        public int ToiletPaper;
        public bool IsChallenge;

        internal Score GetScore()
        {
            League league = __data.GetThisWeeksLeague();
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