using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Extensions;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [EnableCors("AllowAllCorsPolicy")]
    [ApiController]
    [Route("api")]
    [Authorize]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerRepository repository;

        public WorkerController(IWorkerRepository repository)
        {
            this.repository = repository;
        }

        //@method   GET
        //@route    /api/workers
        //@desc     GET All workers from database
        [HttpGet]
        [Route("workers")]
        public async Task<IEnumerable<WorkerDto>> GetWorkers() => (await repository.GetWorkers()).Select(worker => worker.AsDto());

        //@method   GET
        //@route    /api/workers/{id}
        //@desc     GET Worker with {id} from database
        [HttpGet]
        [Route("workers/{id}")]
        public async Task<ActionResult<WorkerDto>> GetWorker(string id)
        {
            var worker = await repository.GetWorker(id);

            if (worker is null)
            {
                return NotFound();
            }

            return worker.AsDto();
        }

        //@method   POST
        //@route    /api/workers
        //@desc     CREATE Worker and add to database collection
        [HttpPost]
        [Route("workers")]
        public async Task<ActionResult> CreateWorker(CreateWorkerDto worker)
        {

            var workers = (await repository.GetWorkers()).ToList();
            var existing = workers.Find(w => w.userId == worker.userId);
            if (existing is not null)
            {
                return BadRequest();
            }

            Worker newWorker = new()
            {
                Id = worker.Id,
                FullName = worker.FullName,
                Position = worker.Position,
                userId = worker.userId,
                Company = worker.Company,
                Grades = worker.Grades
            };
            await repository.CreateWorker(newWorker);
            return Ok(new { worker });
        }

        //@method   PUT
        //@route    /api/workers/{id}
        //@desc     UPDATE Worker with {id} from database
        [HttpPut]
        [Route("workers/{id}")]
        public async Task<ActionResult> UpdateWorker(string id, UpdateWorkerDto worker)
        {
            var existing = await repository.GetWorker(id);
            if (existing is null)
            {
                return NotFound();
            }
            Worker updatedWorker = existing with
            {
                Id = existing.Id,
                FullName = worker.FullName,
                Position = worker.Position,
                userId = worker.userId,
                Company = worker.Company,
                Grades = worker.Grades
            };
            await repository.UpdateWorker(updatedWorker);
            return Ok(new { updatedWorker });
        }

        //@method   DELETE
        //@route    /api/workers/{id}
        //@desc     DELETE Worker with {id} from database
        [HttpDelete]
        [Route("workers/{id}")]
        public async Task<ActionResult> DeleteWorker(string id)
        {
            var existing = await repository.GetWorker(id);
            if (existing is null)
            {
                return NotFound();
            }
            await repository.DeleteWorker(id);
            return Ok("Deleted");
        }


        //@method   POST
        //@route    /api/workers/{id}/grade
        //@desc     Add grade to worker with {id}
        [HttpPost("{id}/{grade}")]
        [Route("workers/{id}/grade")]

        public async Task<ActionResult> GradeWorker(string id, Grade grade)
        {
            Worker worker = await repository.GetWorker(id);

            if (worker is null)
            {
                return NotFound();
            }

            worker = await repository.GradeWorker(id, grade);
            return Ok(new { worker });
        }

        //@method   PUT
        //@route    /api/workers/{id}/grade/{gradeId}
        //@desc     UPDATE Worker with {id} from database
        [HttpPut]
        [Route("workers/{id}/grade/{gradeId}")]
        public async Task<ActionResult> UpdateGrade(string id, string gradeId, Grade grade)
        {
            Worker worker = await repository.GetWorker(id);
            if (worker is null)
            {
                return NotFound();
            }
            worker = await repository.UpdateGrade(id, gradeId, grade);
            return Ok(new { worker });
        }

        //@method   PUT
        //@route    /api/workers/{id}/grade/{gradeId}
        //@desc     UPDATE Worker with {id} from database
        [HttpDelete]
        [Route("workers/{id}/grade/{gradeId}")]
        public async Task<ActionResult> DeleteGrade(string id, string gradeId)
        {
            Worker worker = await repository.GetWorker(id);
            if (worker is null)
            {
                return NotFound();
            }
            worker = await repository.DeleteGrade(id, gradeId);
            return Ok(new { worker });
        }

        //@method   GET
        //@route    /api/workers/user/{id}
        //@desc     GET Worker with user {id} from database

        [HttpGet]
        [Route("workers/user/{id}")]
        public async Task<ActionResult> GetWorkerFromUserId(string id)
        {
            var worker = await repository.GetWorkerFromUserId(id);
            if(worker is null)
            {
                return NotFound();
            }

            return Ok(new{worker});
        }
    }
}