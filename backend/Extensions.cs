using Backend.Dtos;
using Backend.Models;

namespace Backend.Extensions
{
    public static class Extensions
    {
        public static WorkerDto AsDto(this Worker worker)
        {
            return new WorkerDto()
            {
              Id = worker.Id,
              FullName = worker.FullName,
              Position = worker.Position,
              Company = worker.Company,
              userId = worker.userId,
              Grades = worker.Grades  
            };
        }
    }
}