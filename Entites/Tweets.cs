using System;
using System.ComponentModel.DataAnnotations;

namespace TweetApp.Entites
{
    public class Tweets
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Tweet { get;set ;}

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
