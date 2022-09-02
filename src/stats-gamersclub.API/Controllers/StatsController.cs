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
        public async Task<IActionResult> RetrieveMonthsStatsOfPlayers([FromBody] GetMonthFromPlayerStatsCommand playerIds) {
            var response = await _mediator.Send(playerIds);
            return this.ProcessResult(response);
        }

        [HttpPost]
        [Route("players")]
        public async Task<IActionResult> RetrievePlayersStatsByMonth([FromBody] GetPlayersStatsCommand playersCompare) {
            var response = await _mediator.Send(playersCompare);
            return this.ProcessResult(response);
        }
    }
}