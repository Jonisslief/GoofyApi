using Domain.Entities;

namespace DAL.Interfaces
{
    public interface IBenchRepository
    {
        Task AddAsync(Bench bench);
        Task DeleteAsync(int id);
        Task<List<Bench>> GetAllAsync();
        Task<Bench> GetByIdAsync(int id);
        Task<List<Bench>> GetByUserAsync(int id);
        Task<Score> GetScoreAsync(int benchId);
        Task UpdateBenchAsync(Bench bench);
        Task UpdateScoreAsync(Score score);
    }
}