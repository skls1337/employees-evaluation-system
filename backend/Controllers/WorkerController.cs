using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Extensions;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("workers")]
    public class WorkerController:ControllerBase
    {
        private readonly IWorkerRepository repository;

        public WorkerController(IWorkerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<WorkerDto>> GetWorkers() => (await repository.GetWorkers()).Select(worker => worker.AsDto());

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerDto>> GetWorker(string id)
        {
            var worker = await repository.GetWorker(id);
            
            if(worker is null)
            {
                return NotFound();
            }

            return  worker.AsDto();
        }
        [HttpPost]
        public async Task<ActionResult> CreateWorker(CreateWorkerDto worker)
        {
            Worker newWorker = new()
            {
                Id = worker.Id,
                FullName = worker.FullName,
                Position = worker.Position,
                Grades = worker.Grades
            };
            await repository.CreateWorker(newWorker);
            return Ok(new {worker});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWorker(string id,UpdateWorkerDto worker)
        {
            var existing = await repository.GetWorker(id);
            if(existing is null)
            {
                return NotFound();
            }
            Worker updatedWorker = new Worker()
            {
                Id = existing.Id,
                FullName = worker.FullName,
                Position = worker.Position,
                Grades = worker.Grades
            };
            await repository.UpdateWorker(updatedWorker);
            return Ok(new{updatedWorker});
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorker(string id)
        {
            var existing = await repository.GetWorker(id);
            if(existing is null)
            {
                return NotFound();
            }
            await repository.DeleteWorker(id);
            return Ok("Deleted");
        }

    }
}