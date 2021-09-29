using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(string id);
        Task<User> GetUserByEmail(string email);
        Task<string> Register(string email, string password,Role role);
        Task<string> Login(string email, string password);

    } 
}