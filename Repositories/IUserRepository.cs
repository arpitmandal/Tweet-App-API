using TweetApp.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TweetApp.Repositories
{
    public interface IUserRepository
    {
        public Task<Users> EmailValidation(string emailId);
        public Task<int> UserRegistration(Users user);
        public Task<Users> Login(string userName, string password);
        public Task<int> ForgotPassword(string email, string newPassword);
        public Task<int> ResetPassword(string userName, string oldPassword, string newPassword);
        public Task<IList<Users>> GetAllUsers();
    }
}
