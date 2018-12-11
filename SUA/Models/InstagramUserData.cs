using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class InstagramUserData
    {
        public string InstagramUser { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public double EngagementRate { get; set; }
        public double AvarageLikes { get; set; }
        public double AvarageComments { get; set; }
        public int Posts { get; set; }
        public List<FollowTimeLine> FollowersTimeLine { get; set; }
        public List<FollowTimeLine> PostsTimeLine { get; set; }
        public List<TimeLineCount> FollowingTimeLine { get; set; }
        public bool IsVerified { get; set; }
        public Uri Picture { get; set; }
    }

    public class FollowTimeLine : TimeLineCount
    {
        public int Difference { get; set; }
    }

    public class TimeLineCount
    {
        public DateTime Fecha { get; set; }
        public string DayOfTheWeek
        {
            get
            {
                return Fecha.DayOfWeek.ToString();
            }
            set { }
        }
        public int Cantidad { get; set; }

    }
}