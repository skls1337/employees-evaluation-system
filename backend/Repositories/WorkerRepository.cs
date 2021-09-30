using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using MongoDB.Driver;

namespace Backend.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly IMongoCollection<Worker> workerCollection;
        private const string dbName = "employees-evaluation";
        private const string collectionName = "workers";
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
            var filter = filterBuilder.Eq(existing => existing.Id, worker.Id);
            await workerCollection.ReplaceOneAsync(filter, worker);
        }

        public async Task DeleteWorker(string id)
        {
            var filter = filterBuilder.Eq(existing => existing.Id, id);
            await workerCollection.DeleteOneAsync(filter);
        }

        public async Task CreateWorker(Worker worker) => await workerCollection.InsertOneAsync(worker);

        public async Task<Worker> GradeWorker(string id, Grade grade)
        {
            var worker = await GetWorker(id);
            worker.Grades.Add(grade);
            await UpdateWorker(worker);
            return worker;
        }

        public async Task<Worker> UpdateGrade(string id, string gradeId, Grade newGrade)
        {
            var worker = await GetWorker(id);
            var index = worker.Grades.FindIndex(i => i.Id == gradeId);
            worker.Grades[index] = newGrade;
            await UpdateWorker(worker);
            return worker;
        }

        public async Task<Worker> DeleteGrade(string id, string gradeId)
        {
            var worker = await GetWorker(id);
            var index = worker.Grades.FindIndex(i => i.Id == gradeId);
            worker.Grades.RemoveAt(index);
            await UpdateWorker(worker);
            return worker;
        }

        public async Task<Worker> GetWorkerFromUserId(string id)=> await workerCollection.Find<Worker>(w => w.userId == id).SingleOrDefaultAsync();
    }
}