using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Models
{
    public class InstagramUserInfoResponse
    {
        public EntryData entry_data { get; set; }
        public string User { get; set; }
    }

    public class EntryData
    {
        public ProfilePage[] ProfilePage { get; set; }
    }

    public class ProfilePage
    {
        public Graphql graphql { get; set; }
    }
    public class Graphql
    {
        public User user { get; set; }
    }

    public class User
    {
        public PeopleCount edge_followed_by { get; set; }
        public PeopleCount edge_follow { get; set; }
        public bool has_channel { get; set; }
        public bool is_verified { get; set; }
        public string profile_pic_url { get; set; }
        public int MyProperty { get; set; }
    }

    public class PeopleCount
    {
        public int count { get; set; }
    }
}