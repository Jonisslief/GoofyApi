using DAL.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class BenchRepository : IBenchRepository
    {
        private readonly DataContext _context;
        public BenchRepository(DataContext data)
        {
            _context = data;
        }

        public async Task<List<Bench>> GetAllAsync()
        {
            return await _context.Benches.Include(x => x.User).Include(y => y.Score).Include(z => z.Photos).ToListAsync();
        }

        public async Task<Bench> GetByIdAsync(int id)
        {
            return await _context.Benches.Include(x => x.User).Include(y => y.Score).Include(z => z.Photos).FirstOrDefaultAsync(z => z.Id == id);
        }

        public async Task<List<Bench>> GetByUserAsync(int id)
        {
            return await _context.Benches.Where(x => x.UserId == id).Include(x => x.Score).Include(y => y.User).Include(z => z.Photos).ToListAsync();
        }

        public async Task<Score> GetScoreAsync(int benchId)
        {
            return await _context.Scores.FirstOrDefaultAsync(x => x.BenchId == benchId);
        }

        public async Task AddAsync(Bench bench)
        {
            await _context.Benches.AddAsync(bench);
            //await _context.Scores.AddAsync(bench.Score);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBenchAsync(Bench bench)
        {
            _context.Benches.Update(bench);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateScoreAsync(Score score)
        {
            _context.Scores.Update(score);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bench = await GetByIdAsync(id);
            if (bench != null)
            {
                var score = await GetScoreAsync(bench.Id);
                _context.Benches.Remove(bench);
                _context.Scores.Remove(score);
                await _context.SaveChangesAsync();
            }
        }
    }
}
