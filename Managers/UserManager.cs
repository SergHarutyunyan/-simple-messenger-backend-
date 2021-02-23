using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MessengerAPI.Connectivity;
using MessengerAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace MessengerAPI.Managers
{
    public class UserManager
    {
        private Context _dbContext;

        public UserManager(Context context){
            _dbContext = context;
        }

        public async Task<List<string>> GetAllUsers(string currentUser) {
            List<string> usernameList = new List<string>();

            foreach(User user in await _dbContext.Users.ToListAsync()) {
                if(user.Username != currentUser)
                    usernameList.Add(user.Username);
            }            

            return usernameList;
        }

        public async Task<User> AuthenticateUser(string email, string password){            
       
            var user = await Task.Run(() => _dbContext.Users.FirstOrDefault(x => (x.Email == email && x.Password == password)));

            // return null if user not found
            if (user == null)
                return null;

            user.AuthenticationData = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", user.Email, user.Username, user.Password)));

            return user;
            
        }

        public async Task<User> FindUser(string email, string password) {

            string hashedPassword = hashPassword(password);

            var user = await Task.Run(() =>
            {
                return _dbContext.Users.Where(u => u.Email == email && u.Password == hashedPassword).FirstOrDefault();
            });

            if(user != null)
                user.AuthenticationData = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", user.Email, user.Username, user.Password)));

            return user;
        }

        public async Task<User> CreateUser(string email, string username, string password) 
        {          
            var user = await Task.Run(() =>
            {
                return _dbContext.Users.Where(u => u.Email == email).FirstOrDefault();
            });

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    Username = username,
                    Password = hashPassword(password),
                    RegistrationDate = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss")
                };

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                
                user.AuthenticationData = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", user.Email, user.Username, user.Password)));

                return user;
            }

            return null;           
        }

        private string hashPassword(string password){
               
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128];
            for (int i = 0; i < salt.Length; i++)
            {
                salt[i] = (byte)i;
            }
            
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}