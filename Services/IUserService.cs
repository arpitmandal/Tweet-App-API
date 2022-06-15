using TweetApp.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TweetApp.Services
{
    public interface IUserService
    {
        public Task<string> UserRegistration(Users user);
        public Task<Users> Login(string userName, string password);
        public Task<string> ForgotPassword(string email, string newPassword);
        public Task<string> ResetPassword(string email, string oldPassword, string newPassword);
        public Task<IList<Users>> GetAllUsers();
    }
}
