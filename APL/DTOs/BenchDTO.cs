using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace APL.DTOs
{
    public class BenchDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public Double? LocationX { get; set; }
        public Double? LocationY { get; set; }

        public List<PhotoDTO>? Photos { get; set; }

        public ScoreDTO Score { get; set; }

        public UserDTO? User { get; set; }
        public int UserId { get; set; }
    }
}
