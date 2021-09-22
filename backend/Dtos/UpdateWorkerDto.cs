using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.Dtos
{
    public record UpdateWorkerDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Position { get; set; }
        public List<Grade> Grades { get; set; }
    }
}