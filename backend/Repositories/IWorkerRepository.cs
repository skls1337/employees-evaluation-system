using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repositories
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetWorkers();
        Task<Worker> GetWorker(string id);
        Task CreateWorker(Worker worker);
        Task UpdateWorker(Worker worker);
        Task DeleteWorker(string id); 
    }
}