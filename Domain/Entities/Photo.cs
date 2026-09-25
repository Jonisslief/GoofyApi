using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public string Description { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }
        [ForeignKey(nameof(Bench))]
        public int BenchId { get; set; }
        public Bench Bench { get; set; }
    }
}
