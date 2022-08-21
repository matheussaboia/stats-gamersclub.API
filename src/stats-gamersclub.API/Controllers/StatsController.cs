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

        [HttpPost]
        [Route("players/months")]
        public async Task<IActionResult> RetrieveMonthsOfStatsFromPlayer([FromBody] GetMonthFromPlayerStats playerIds) {
            var response = await _mediator.Send(playerIds);
            return this.ProcessResult(response);
        }

        //[HttpPost]
        //[Route("players/{playerId}/month/{month}")]
        //public async Task<IActionResult> RetrieveStatsFromPlayer(string playerId, string month) {
        //    var response = await _mediator.Send(new GetPlayerStatsCommand { PlayerId = playerId, Month = month });
        //    return this.ProcessResult(response);
        //}

        [HttpPost]
        [Route("players")]
        public async Task<IActionResult> RetrieveStatsFromPlayers([FromBody] GetPlayersStatsToCompareCommand playersCompare) {
            var response = await _mediator.Send(playersCompare);
            return this.ProcessResult(response);
        }
    }
}