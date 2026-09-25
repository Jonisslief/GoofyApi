using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IBenchManager
    {
        Task AddAsync(Bench bench);
        Task DeleteAsync(int id);
        Task<List<Bench>> GetAllAsync();
        Task<Bench> GetByIdAsync(int id);
        Task<List<Bench>> GetByUserAsync(int id);
        Task UpdateBenchAsync(Bench bench);
        Task UpdateScoreAsync(Score score);
    }
}
