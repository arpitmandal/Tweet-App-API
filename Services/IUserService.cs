using TweetApp.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TweetApp.Services
{
    public interface IUserService
    {
        public Task<string> UserRegistration(User user);
        public Task<User> Login(string userName, string password);
        public Task<string> ForgotPassword(string email, string newPassword);
        public Task<string> ResetPassword(string email, string oldPassword, string newPassword);
        public Task<IList<User>> GetAllUsers();
    }
}
