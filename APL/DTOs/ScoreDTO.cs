namespace APL.DTOs
{
    public class ScoreDTO
    {
        public int? Id {  get; set; }
        public int Location { get; set; } = 0;
        public int View { get; set; } = 0;
        public int Bench { get; set; } = 0;
        public int Privacy { get; set; } = 0;
        public int? Average { get; set; } = 0;
        public int? BenchId { get; set; }
    }
}
