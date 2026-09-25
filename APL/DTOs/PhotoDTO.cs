namespace APL.DTOs
{
    public class PhotoDTO
    {
        public int? Id {  get; set; }
        public byte[] Bytes { get; set; }
        public string Description { get; set; }
        public string FileExtention { get; set; }
        public decimal Size { get; set; }
        public int? BenchId { get; set; }
    }
}
