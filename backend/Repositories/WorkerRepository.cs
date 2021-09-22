using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using MongoDB.Driver;

namespace Backend.Repositories
{
    public class WorkerRepository:IWorkerRepository
    {
        private readonly IMongoCollection<Worker> workerCollection;
        private const string dbName = "employees-evaluation";
        private const string collectionName= "workers";
        private readonly FilterDefinitionBuilder<Worker> filterBuilder = Builders<Worker>.Filter; 

        public WorkerRepository(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(dbName);
            workerCollection = database.GetCollection<Worker>(collectionName);
        }

        public async Task<IEnumerable<Worker>> GetWorkers() => await workerCollection.Find<Worker>(worker => true).ToListAsync();

        public async Task<Worker> GetWorker(string id) => await workerCollection.Find<Worker>(worker => worker.Id == id).SingleOrDefaultAsync();

        public async Task UpdateWorker(Worker worker)
        {
            var filter = filterBuilder.Eq(existing => existing.Id,worker.Id);
            await workerCollection.ReplaceOneAsync(filter,worker);
        }

        public async Task DeleteWorker(string id)
        {
             var filter = filterBuilder.Eq(existing => existing.Id,id);
            await workerCollection.DeleteOneAsync(filter);
        }

        public async Task CreateWorker(Worker worker) => await workerCollection.InsertOneAsync(worker);
    }
}