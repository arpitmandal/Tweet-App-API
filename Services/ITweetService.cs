using TweetApp.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TweetApp.Services
{
    public interface ITweetService
    {
        public Task<IList<Tweets>> GetTweetsByUserName(string userName);
        public Task<IList<Tweets>> GetAllTweets();
        public Task<string> AddTweet(Tweets tweet);
        public Task<bool> DeleteTweet(int tweetId);

    }
}
