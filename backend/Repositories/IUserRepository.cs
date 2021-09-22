using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repositories
{
    public interface IUserRepository
    {
        Task<string> Register(string email, string password);
        Task<string> Login(string email, string password);

    } 
}