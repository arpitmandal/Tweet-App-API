using TweetApp.Entites;
using TweetApp.Repositories;
using TweetApp.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TweetApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _accountRepository;

        public UserService(IUserRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        public async Task<string> UserRegistration(User user)
        {
            string message;

            var validateEmail = await this._accountRepository.EmailValidation(user.Email);

            if (validateEmail == null)
            {
                var result = await this._accountRepository.UserRegistration(user);

                if (result > 0)
                {
                    message = "Registered!";
                }
                else
                {
                    message = "Unsuccessful Registration!";
                }
            }
            else
            {
                message = "Email Id ";
            }

            return message;
        }
        public async Task<User> Login(string userName, string password)
        {
            User user = await this._accountRepository.Login(userName, password);
            return user;
        }
        public async Task<string> ForgotPassword(string email, string newPassword)
        {
            string message;
            var validateEmail = await this._accountRepository.EmailValidation(email);
            if (validateEmail != null)
            {
                int result = await this._accountRepository.ForgotPassword(email, newPassword);
                if (result > 0)
                {
                    message = "Password changed.";
                }
                else
                {
                    message = "Password cannot be changed.";
                }
            }
            else
            {
                message = "Email Id not valid.";
            }

            return message;
        }

        public async Task<string> ResetPassword(string userName, string oldPassword, string newPassword)
        {
            string message;
            int result = await this._accountRepository.ResetPassword(userName, oldPassword, newPassword);
            if (result > 0)
            {
                message = "Password changed";
            }
            else
            {
                message = "Password cannot be changed";
            }
            return message;
        }

        public async Task<IList<User>> GetAllUsers()
        {
            var result = await this._accountRepository.GetAllUsers();
            return result;
        }
    }
}
