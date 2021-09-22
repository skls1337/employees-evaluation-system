using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Backend.Utils;
using System.Diagnostics;
using System.Collections.Generic;

namespace Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string collectionName = "users";
        private const string dbName = "employees-evaluation";
        private readonly string key;
        private readonly IMongoCollection<User> userCollection;

        public UserRepository(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(dbName);
            userCollection = database.GetCollection<User>(collectionName);
            this.key = "lsdoaidioasidiasd";
        }

        public async Task<User> GetUser(string id) =>await userCollection.Find<User>(user=>user.Id==id).SingleOrDefaultAsync();

        public async Task<IEnumerable<User>> GetUsers()=>await userCollection.Find<User>(user=>true).ToListAsync();
        

        public async Task<string> Login(string email, string password)
        {
            var user = await userCollection.Find(user => user.Email == email).SingleOrDefaultAsync();
            if (user is null)
            {
                return null;
            }
           string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            
            bool verified = BCrypt.Net.BCrypt.Verify(password, passwordHash);
            
            if(!verified)
            {
                return null;
            }
            return Utils.Utils.GenerateJwt(key,email,password);
        }

        public async Task<string> Register(string email, string password)
        {
            var user = await userCollection.Find(user => user.Email == email).SingleOrDefaultAsync();

            if (user is not null)
            {
                return null;
            }
            
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            
            user = new()
            {
                Email = email,
                Password = passwordHash
            };
            await userCollection.InsertOneAsync(user);
            return Utils.Utils.GenerateJwt(key,email,password);
        }
    }
}