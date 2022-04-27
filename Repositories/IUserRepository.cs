using TweetApp.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TweetApp.Repositories
{
    public interface IUserRepository
    {
        public Task<User> EmailValidation(string emailId);
        public Task<int> UserRegistration(User user);
        public Task<User> Login(string userName, string password);
        public Task<int> ForgotPassword(string email, string newPassword);
        public Task<int> ResetPassword(string userName, string oldPassword, string newPassword);
        public Task<IList<User>> GetAllUsers();
    }
}
