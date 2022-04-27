using TweetApp.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Entites.TweetDbContext _dbContext;

        public UserRepository(Entites.TweetDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> UserRegistration(User user)
        {
            try
            {
                this._dbContext.User.Add(user);
                var result = await this._dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> Login(string userName, string password)
        {
            try
            {
                var user = await this._dbContext.User.Where(e => e.Email == userName && e.Password == password).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> EmailValidation(string email)
        {
            try
            {
                var user = await this._dbContext.User.Where(x => x.Email == email).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ForgotPassword(string email, string newPassword)
        {
            try
            {
                int response = 0;
                var result = await this._dbContext.User.Where(s => s.Email == email).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.Password = newPassword;
                    this._dbContext.Update(result);
                    response = this._dbContext.SaveChanges();
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<int> ResetPassword(string email, string oldPassword, string newPassword)
        {
            try
            {
                int response = 0;
                var result = await this._dbContext.User.Where(s => s.Email == email && s.Password == oldPassword).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.Password = newPassword;
                    this._dbContext.Update(result);
                    response = this._dbContext.SaveChanges();
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<User>> GetAllUsers()
        {
            var result = await this._dbContext.User.Select(p => new User
            {
                First_Name = p.First_Name,
                Last_Name = p.Last_Name,
                Email = p.Email,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                Id = p.Id
            }).ToListAsync();

            return result;
        }
    }
}
