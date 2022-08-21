using MediatR;
using Microsoft.AspNetCore.Mvc;
using stats_gamersclub.API.Configurations.Extensions;
using stats_gamersclub.Domain.Commands;

namespace stats_gamersclub.API.Controllers {

    [Route("api/v1/stats")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> _logger;
        private readonly IMediator _mediator;

        public StatsController(ILogger<StatsController> logger, IMediator mediator) {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("players/{playerId}/month/{month}")]
        public async Task<IActionResult> GetStatsFromPlayer(string playerId, string month) {
            var response = await _mediator.Send(new GetPlayerStatsCommand { PlayerId = playerId, Month = month });
            return this.ProcessResult(response);
        }

        [HttpPost]
        [Route("/players/compare")]
        public async Task<IActionResult> GetStatsFromPlayersToCompare([FromBody] GetPlayersStatsToCompareCommand playersCompare) {

            var response = await _mediator.Send(playersCompare);
            return this.ProcessResult(response);
        }
    }
}