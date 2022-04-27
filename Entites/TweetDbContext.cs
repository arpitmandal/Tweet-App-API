using Microsoft.EntityFrameworkCore;

namespace TweetApp.Entites
{
    public class TweetDbContext : DbContext
    {
        public TweetDbContext(DbContextOptions<TweetDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Tweets> Tweets { get; set; }
    }
}
