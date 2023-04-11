using DataBackend.Database;
using DataBackend.Entities;
using Microsoft.EntityFrameworkCore;
using ServicesBackend.Model.InputModel;
using ServicesBackend.Services.Base;
using System.Security.Cryptography;
using System.Text;

namespace ServicesBackend.Services
{
    public class UserService : DbService<AppDBContext>, IUserService
    {
        public UserService(DbContextOptions<AppDBContext> dbContextOptions)
        : base(dbContextOptions) { }

        public async Task<string> RegisterAsync(UserRegisterInputModel userInputModel)
        {
            if(await IsUserExistAsync(userInputModel.Name))
            {
                throw new Exception("User is already registered!");
            }

            if(userInputModel.Password != userInputModel.ConfirmPassword)
            {
                throw new Exception("Both passwords must be equal!");
            }

            CreatePasswordHash(userInputModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new UserEntity()
            {
                Name = userInputModel.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            using var dbContext = CreateDbContext();
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return user.Id.ToString();
        }


        public async Task<bool> LoginAsync(UserLoginInputModel userInputModel)
        {
            using var dbContext = CreateDbContext();

            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == userInputModel.Name);

            if(user == null)
            {
                throw new Exception("User name is incorrect");
            }

            if (!VerifyPasswordHash(userInputModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Password is incorrect");
            }

            return true;
        }

        #region Private
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private async Task<bool> IsUserExistAsync(string name)
        {
            using var dbContext = CreateDbContext();
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == name);

            if (user == null)
                return false;
            return true;
        }
        #endregion
    }
}
