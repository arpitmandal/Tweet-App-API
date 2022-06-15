using TweetApp.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TweetApp.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private TweetDbContext context;

        public TweetRepository(TweetDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<Tweets>> GetTweetsByUser(string userName)
        {
            try
            {
                var tweets = await (from tweet in context.Tweets
                                    join user in context.Users on tweet.UserId equals user.Id
                                    where user.Email == userName
                                    select new Tweets
                                    {
                                        Id = tweet.Id,
                                        UserId = user.Id,
                                        UserName = user.Email,
                                        Tweet = tweet.Tweet,
                                        CreatedOn = tweet.CreatedOn,
                                        ModifiedOn = tweet.ModifiedOn
                                    }).ToListAsync();
                return tweets;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<Tweets>> GetAllTweets()
        {
            try
            {
                var tweetList = await (from tweet in context.Tweets
                                       join user in context.Users on tweet.UserId equals user.Id
                                       select new Tweets
                                       {
                                           Id = tweet.Id,
                                           UserId = user.Id,
                                           UserName = user.Email,
                                           Tweet = tweet.Tweet,
                                           CreatedOn = tweet.CreatedOn,
                                           ModifiedOn = tweet.ModifiedOn
                                       }).OrderByDescending(e => e.CreatedOn).ToListAsync();
                return tweetList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddTweet(Tweets tweet)
        {
            try
            {
                tweet.CreatedOn = DateTime.UtcNow;
                tweet.ModifiedOn = DateTime.UtcNow;
                this.context.Tweets.Add(tweet);
                var result = await this.context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> DeleteTweet(int tweetId)
        {
            int res = 0;
            try
            {
                var tweet = await context.Tweets.Where(e => e.Id == tweetId).FirstOrDefaultAsync();
                context.Remove(tweet);
                res = await context.SaveChangesAsync();
                if (res > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
