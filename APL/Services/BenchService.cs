using APL.DTOs;
using APL.Interfaces;
using BLL.Interfaces;
using Domain.Entities;
using NetTopologySuite.Geometries;
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
                b.Location ??= new Point(0, 0);
                var photos = new List<PhotoDTO>();
                foreach (var photo in b.Photos)
                {
                    photos.Add(new PhotoDTO
                    {
                        Id = photo.Id,
                        Bytes = photo.Bytes,
                        Description = photo.Description,
                        FileExtention = photo.FileExtension,
                        Size = photo.Size,
                        BenchId = b.Id
                    });
                }
                benchesDto.Add(new BenchDTO
                {
                    Name = b.Name,
                    User = new UserDTO { Id = userId, Name = user.Name },
                    LocationX = b.Location.Coordinate.X,
                    LocationY = b.Location.Coordinate.Y,
                    Score = new ScoreDTO { Id = score.Id, Bench = score.Bench, Location = score.Location, Privacy = score.Privacy, View = score.View, Average = (score.View + score.Privacy + score.Bench + score.Location) / 4 },
                });
            });
            return benchesDto;
        }

        public async Task<BenchDTO> GetBenchAsync(int id)
        {
            var bench = await _bench.GetByIdAsync(id);
            var user = await _user.GetByIdAsync(bench.UserId);
            bench.Location ??= new Point(0, 0);
            var score = bench.Score;
            var photos = new List<PhotoDTO>();
            foreach (var photo in bench.Photos)
            {
                photos.Add(new PhotoDTO
                {
                    Id = photo.Id,
                    Bytes = photo.Bytes,
                    Description = photo.Description,
                    FileExtention = photo.FileExtension,
                    Size = photo.Size,
                    BenchId = bench.Id
                });
            }
            var dto = new BenchDTO
            {
                Id = id,
                Name = bench.Name,
                LocationX = bench.Location.Coordinate.X,
                LocationY = bench.Location.Coordinate.Y,
                Photos = photos,
                Score = new ScoreDTO { Id = score.Id, Bench = score.Bench, Location = score.Location, Privacy = score.Privacy, View = score.View, Average = (score.View + score.Privacy + score.Bench + score.Location) / 4 },
                User = new UserDTO { Id =  user.Id, Name = user.Name }
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
                x.Location ??= new Point(0, 0);
                var photos = new List<PhotoDTO>();
                foreach (var photo in x.Photos)
                {
                    photos.Add(new PhotoDTO
                    {
                        Id = photo.Id,
                        Bytes = photo.Bytes,
                        Description = photo.Description,
                        FileExtention = photo.FileExtension,
                        Size = photo.Size,
                        BenchId = x.Id
                    });
                }
                benchesDTO.Add(new BenchDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    LocationX = x.Location.Coordinate.X,
                    LocationY = x.Location.Coordinate.Y,
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
            addBench.Photos.ForEach(b => photos.Add(new Photo { Bytes = b.Bytes, Description = b.Description, FileExtension = b.FileExtention, Size = b.Size}));
            var bench = new Bench
            {
                UserId = (int)addBench.UserId!,
                Name = addBench.Name,
                Score = score,
                Location = new Point(addBench.LocationX, addBench.LocationY) { SRID = 4326 },
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
    }
}
