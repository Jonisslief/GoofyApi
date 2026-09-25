using APL.DTOs;
using APL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "User,Admin")]
    public class BenchController : ControllerBase
    {
        private readonly IBenchService _bench;

        public BenchController(IBenchService bench)
        {
            _bench = bench;
        }

        [HttpGet]
        [ProducesResponseType<List<BenchDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentUserBenches()
        {
            var userClaims = HttpContext.User.Claims;
            var userId = userClaims.First(c => c.Type == "UserID").Value;
            var task = _bench.GetUserBenches(int.Parse(userId));

            var benches = await task;
            if (task.IsCompletedSuccessfully)
            {
                return Ok(benches);
            }
            return BadRequest(task.Exception);
        }

        [HttpGet]
        [ProducesResponseType<BenchDTO>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBenchAsync(int id)
        {
            var task = _bench.GetBenchAsync(id);

            var bench = await task;
            if (task.IsCompletedSuccessfully)
            {
                return Ok(bench);
            }
            return BadRequest(task.Exception);
        }

        [HttpGet]
        [ProducesResponseType<List<BenchDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBenches()
        {
            var task = _bench.GetAllAsync();

            var benches = await task;
            if (task.IsCompletedSuccessfully)
            {
                return Ok(benches);
            }
            return BadRequest(task.Exception);
        }

        [HttpPost]
        public async Task<IActionResult> AddBench([FromBody] AddBenchDTO bench)
        {
            var task = _bench.AddBench(bench);
            await task;
            if (task.IsCompletedSuccessfully)
            {
                return Created();
            }
            return BadRequest(task.Exception);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBench(int id)
        {
            var task = _bench.DeleteAsync(id);
            await task;
            if (task.IsCompletedSuccessfully)
            {
                return Ok();
            }
            return BadRequest(task.Exception);
        }

        [HttpPut]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateBench([FromBody] AddBenchDTO bench)
        {
            bench.Score.BenchId = bench.Score.BenchId ?? bench.Id;
            var task = _bench.UpdateBenchAsync(bench);
            await task;
            if (task.IsCompletedSuccessfully)
            {
                return Ok();
            }
            return BadRequest(task.Exception);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateScore([FromBody] ScoreDTO score)
        {
            var task = _bench.UpdateScoreAsync(score);
            await task;
            if (task.IsCompletedSuccessfully)
            {
                return Ok();
            }
            return BadRequest(task.Exception);
        }
    }
}
