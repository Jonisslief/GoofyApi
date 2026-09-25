using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Score
    {
        [Key]
        public int Id { get; set; }
        public int Location { get; set; } = 0;
        public int View { get; set; } = 0;
        public int Bench { get; set; } = 0;
        public int Privacy { get; set; } = 0;
        public int Average { get; set; } = 0;
        public Bench BenchT { get; set; } = null!;
        [ForeignKey(nameof(BenchT))]
        public int BenchId { get; set; }
    }
}
