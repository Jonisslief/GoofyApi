using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Bench
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Double? LocationX { get; set; }
        public Double? LocationY { get; set; }

        public List<Photo> Photos { get; set; }

        public Score Score { get; set; }

        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

    }
}
