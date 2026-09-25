using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using Domain.Entities;

namespace BLL.Managers
{
    public class BenchManager : IBenchManager
    {
        private readonly IBenchRepository _bench;
        public BenchManager(IBenchRepository bench)
        {
            _bench = bench;
        }

        public async Task<Bench> GetByIdAsync(int id)
        {
            return await _bench.GetByIdAsync(id);
        }

        public async Task<List<Bench>> GetByUserAsync(int id)
        {
            return await _bench.GetByUserAsync(id);
        }

        public async Task<List<Bench>> GetAllAsync()
        {
            return await _bench.GetAllAsync();
        }

        public async Task AddAsync(Bench bench)
        {
            await _bench.AddAsync(bench);
        }

        public async Task DeleteAsync(int id)
        {
            await _bench.DeleteAsync(id);
        }

        public async Task UpdateBenchAsync(Bench bench)
        {
            await _bench.UpdateBenchAsync(bench);
        }

        public async Task UpdateScoreAsync(Score score)
        {
            await _bench.UpdateScoreAsync(score);
        }
    }
}
