using TweetApp.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TweetApp.Repositories
{
    public interface ITweetRepository
    {
        public Task<IList<Tweets>> GetTweetsByUser(string userName);
        public Task<IList<Tweets>> GetAllTweets();
        public Task<int> AddTweet(Tweets tweet);
        public Task<bool> DeleteTweet(int tweetId);
    }
}
