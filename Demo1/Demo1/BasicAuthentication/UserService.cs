using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Demo1.Database;

namespace Demo1.BasicAuthentication
{
    public class UserService
    {
        private MyDbContext dbContext;
        public UserService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool IsAutnenticated(string username, string password)
        {
            var user = dbContext.Users.Find(username);
            if (user != null)
            {
                return user.HashedPassword == ComputeSha256Hash(password);
            }
            return false;
        }

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public void AddUser(string username, string password)
        {
            dbContext.Users.Add(new User
            {
                UserName = username,
                HashedPassword = ComputeSha256Hash(password)
            });
            dbContext.SaveChanges();
        }

        public void UpdatePassword(string username, string currentPassword, string newPassword)
        {
            var user = dbContext.Users.Find(username);
            if (user != null && user.HashedPassword == ComputeSha256Hash(currentPassword))
            {
                user.HashedPassword = ComputeSha256Hash(newPassword);
                dbContext.SaveChanges();
            }
        }
    }
}
