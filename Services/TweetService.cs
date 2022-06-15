using TweetApp.Entites;
using TweetApp.Repositories;
using TweetApp.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TweetApp.Services
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;

        public TweetService(ITweetRepository tweetRepository)
        {
            this._tweetRepository = tweetRepository;
        }

        public async Task<IList<Tweets>> GetTweetsByUserName(string userName)
        {
            var result = await this._tweetRepository.GetTweetsByUser(userName);
            return result;
        }

        public async Task<IList<Tweets>> GetAllTweets()
        {
            var result = await this._tweetRepository.GetAllTweets();
            return result;
        }

        public async Task<string> AddTweet(Tweets tweet)
        {
            var result = await this._tweetRepository.AddTweet(tweet);
            string message = result > 0 ? ValidationMessages.TweetPosted : ValidationMessages.TweetNotPosted;
            return message;
        }

        public async Task<bool> DeleteTweet(int tweetId)
        {
            try
            {

                var res = await _tweetRepository.DeleteTweet(tweetId);
                return res;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

    }
}
