using APL.DTOs;
using APL.Interfaces;
using BLL.Interfaces;
using Domain.Entities;
using static System.Formats.Asn1.AsnWriter;

namespace APL.Services
{
    public class BenchService : IBenchService
    {
        private readonly IBenchManager _bench;
        private readonly IUserManager _user;
        public BenchService(IBenchManager manager, IUserManager user)
        {
            _bench = manager;
            _user = user;
        }

        public async Task<List<BenchDTO>> GetUserBenches(int userId)
        {
            var benches = await _bench.GetByUserAsync(userId);
            var user = await _user.GetByIdAsync(userId);
            var benchesDto = new List<BenchDTO>();
            benches.ForEach(b =>
            {
                var score = b.Score;
                var photos = new List<PhotoDTO>();
                b.LocationX ??= 0;
                b.LocationY ??= 0;
                if (b.Photos.Count > 0)
                {
                    photos.Add(new()
                    {
                        Id = b.Photos[0].Id,
                        Bytes = b.Photos[0].Bytes,
                        Description = b.Photos[0].Description,
                        FileExtention = b.Photos[0].FileExtension,
                        Size = b.Photos[0].Size,
                        BenchId = b.Id
                    });
                    
                }
                benchesDto.Add(new BenchDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                    User = new UserDTO { Id = userId, Name = user.Name },
                    LocationX = b.LocationX,
                    LocationY = b.LocationY,
                    Photos = photos,
                    Score = new ScoreDTO { Id = score.Id, Bench = score.Bench, Location = score.Location, Privacy = score.Privacy, View = score.View, Average = (score.View + score.Privacy + score.Bench + score.Location) / 4 },
                });
            });
            return benchesDto;
        }

        public async Task<BenchDTO> GetBenchAsync(int id)
        {
            var bench = await _bench.GetByIdAsync(id);
            var user = await _user.GetByIdAsync(bench.UserId);
            var photos = new List<PhotoDTO>();
            bench.LocationX ??= 0;
            bench.LocationY ??= 0;
            var score = bench.Score;
            if (bench.Photos.Count > 0)
            {
                photos.Add(new()
                {
                    Id = bench.Photos[0].Id,
                    Bytes = bench.Photos[0].Bytes,
                    Description = bench.Photos[0].Description,
                    FileExtention = bench.Photos[0].FileExtension,
                    Size = bench.Photos[0].Size,
                    BenchId = bench.Id
                });
            }
            var dto = new BenchDTO
            {
                Id = id,
                Name = bench.Name,
                LocationX = bench.LocationX,
                LocationY = bench.LocationY,
                Photos = photos,
                Score = new ScoreDTO { Id = score.Id, Bench = score.Bench, Location = score.Location, Privacy = score.Privacy, View = score.View, Average = (score.View + score.Privacy + score.Bench + score.Location) / 4 },
                User = new UserDTO { Id = user.Id, Name = user.Name }
            };

            return dto;
        }

        public async Task<List<BenchDTO>> GetAllAsync()
        {
            var benches = await _bench.GetAllAsync();
            var benchesDTO = new List<BenchDTO>();
            benches.ForEach(x =>
            {
                var user = x.User;
                var score = x.Score;
                var photos = new List<PhotoDTO>();
                x.LocationX ??= 0;
                x.LocationY ??= 0;
                if (x.Photos.Count > 0)
                {
                    photos.Add(new()
                    {
                        Id = x.Photos[0].Id,
                        Bytes = x.Photos[0].Bytes,
                        Description = x.Photos[0].Description,
                        FileExtention = x.Photos[0].FileExtension,
                        Size = x.Photos[0].Size,
                        BenchId = x.Id
                    });
                }
                benchesDTO.Add(new BenchDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    LocationX = x.LocationX,
                    LocationY = x.LocationY,
                    Score = new ScoreDTO { Id = score.Id, BenchId = x.Id, Bench = score.Bench, Location = score.Location, Privacy = score.Privacy, View = score.View, Average = (score.View + score.Privacy + score.Bench + score.Location) / 4 },
                    Photos = photos,
                    User = new UserDTO { Id = user.Id, Name = user.Name },
                    UserId = user.Id
                });
            });
            return benchesDTO;
        }

        public async Task AddBench(AddBenchDTO addBench)
        {
            var score = new Score
            {
                Location = addBench.Score.Location,
                Privacy = addBench.Score.Privacy,
                View = addBench.Score.View,
                Bench = addBench.Score.Bench,
                Average = (addBench.Score.View + addBench.Score.Privacy + addBench.Score.Bench + addBench.Score.Location) / 4
            };
            var photos = new List<Photo>();
            addBench.Photos.ForEach(b => photos.Add(new Photo { Bytes = b.Bytes, Description = b.Description, FileExtension = b.FileExtention, Size = b.Size }));
            var bench = new Bench
            {
                UserId = (int)addBench.UserId!,
                Name = addBench.Name,
                Score = score,
                LocationX = addBench.LocationX,
                LocationY = addBench.LocationY,
                Photos = photos
            };
            await _bench.AddAsync(bench);
        }

        public async Task DeleteAsync(int id)
        {
            await _bench.DeleteAsync(id);
        }

        public async Task UpdateBenchAsync(AddBenchDTO updateBench)
        {
            var photos = new List<Photo>();
            updateBench.Photos.ForEach(b => photos.Add(new Photo { Id = (int)b.Id, BenchId = (int)updateBench.Id, Bytes = b.Bytes, Description = b.Description, FileExtension = b.FileExtention, Size = b.Size }));
            var benchNew = new Bench
            {
                Id = (int)updateBench.Id,
                Name = updateBench.Name,
                UserId = (int)updateBench.UserId!,
                LocationX = updateBench.LocationX,
                LocationY = updateBench.LocationY,
                //Location = updateBench.Location,
                Photos = photos
            };
            await UpdateScoreAsync(updateBench.Score);
            await _bench.UpdateBenchAsync(benchNew);
        }

        public async Task UpdateScoreAsync(ScoreDTO score)
        {
            var scoreNew = new Score
            {
                Id = (int)score.Id,
                Bench = score.Bench,
                Location = score.Location,
                Privacy = score.Privacy,
                View = score.View,
                Average = (score.View + score.Privacy + score.Bench + score.Location) / 4,
                BenchId = (int)score.BenchId!
            };

            await _bench.UpdateScoreAsync(scoreNew);
        }

        public async Task<List<PhotoDTO>> GetPhotosAsync(int benchId)
        {
            var photos = await _bench.GetPhotosAsync(benchId);
            var photoDto = new List<PhotoDTO>();
            if (photos.Count > 0)
            {
                photos.ForEach(p => photoDto.Add(new PhotoDTO { Id = p.Id, BenchId = p.BenchId, Bytes = p.Bytes, Description = p.Description, FileExtention = p.FileExtension, Size = p.Size }));
            }
            return photoDto;
        }
    }
}
