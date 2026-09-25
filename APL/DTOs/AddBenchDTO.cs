using Domain.Entities;
using System.Drawing;

namespace APL.DTOs
{
    public class AddBenchDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public Double LocationX { get; set; }
        public Double LocationY { get; set; }

        public List<PhotoDTO>? Photos { get; set; }

        public ScoreDTO Score { get; set; }

        public int UserId { get; set; }
    }
}
