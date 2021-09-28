using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repositories
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetWorkers();
        Task<Worker> GetWorker(string id);
        Task<Worker> GetWorkerFromUserId(string id);
        Task CreateWorker(Worker worker);
        Task UpdateWorker(Worker worker);
        Task DeleteWorker(string id); 
        Task<Worker> GradeWorker(string id,Grade grade);
        Task<Worker> UpdateGrade(string id,string gradeId, Grade newGrade);
        Task<Worker> DeleteGrade(string id, string gradeId);
    }
}