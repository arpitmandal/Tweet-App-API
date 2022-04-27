using TweetApp.Entites;
using TweetApp.Resources;
using TweetApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TweetApp.Controllers
{
    [Route("api/Tweet/")]
    [ApiController]
    [Authorize]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;

        public TweetController(ITweetService tweetService)
        {
            this._tweetService = tweetService;
        }

        [HttpGet]
        [Route("User")]
        public async Task<IActionResult> GetTweetsByUserName(string userName)
        {
            try
            {
                var result = await this._tweetService.GetTweetsByUserName(userName);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest("Error Occurred. " + ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllTweets()
        {
            try
            {
                var result = await this._tweetService.GetAllTweets();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest("Error Occurred. " + ex.Message);
            }
        }

        [HttpPost]
        [Route("add-tweet")]
        public async Task<IActionResult> AddTweet([FromBody] Tweets tweet)
        {
            try
            {
                var result = await this._tweetService.AddTweet(tweet);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest("Error Occurred. " + ex.Message);
            }
        }

        [Route("delete-tweet")]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteTweet(int tweetId)
        {
            try
            {
                var res = await _tweetService.DeleteTweet(tweetId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
