using APL.DTOs;

namespace APL.Interfaces
{
    public interface IBenchService
    {
        Task AddBench(AddBenchDTO addBench);
        Task DeleteAsync(int id);
        Task<List<BenchDTO>> GetAllAsync();
        Task<BenchDTO> GetBenchAsync(int id);
        Task<List<BenchDTO>> GetUserBenches(int userId);
        Task UpdateBenchAsync(AddBenchDTO updateBench);
        Task UpdateScoreAsync(ScoreDTO score);
    }
}
