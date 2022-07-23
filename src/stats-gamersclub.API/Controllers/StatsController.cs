using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using stats_gamersclub.API.CasosDeUso;
using stats_gamersclub.API.DTO;
using stats_gamersclub.API.Dominio;

namespace stats_gamersclub.API.Controllers
{
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> _logger;

        public StatsController(ILogger<StatsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("/api/stats/players/player/{playerId}")]
        public ActionResult<Player> StatsFromPlayer(string playerId, [FromQuery] string month)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            var configuration = builder.Build();

            var seleniumConfigurations = new SeleniumConfigurations();

            new ConfigureFromConfigurationOptions<SeleniumConfigurations>(
                configuration.GetSection("SeleniumConfigurations"))
                    .Configure(seleniumConfigurations);

            var playerController = new PlayerCasoDeUso(seleniumConfigurations);

            playerController.HomePageLoad();

            playerController.LoadPage(playerId, month);
            Player player = playerController.GetStatsFromPlayer();

            playerController.Exit();

            return player;
        }

        [HttpPost]
        [Route("/api/stats/players/compare")]
        public ActionResult<List<Player>> StatsFromPlayers([FromBody] PlayerCompareDto playerCompareDTO)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            var configuration = builder.Build();

            var seleniumConfigurations = new SeleniumConfigurations();
                
            new ConfigureFromConfigurationOptions<SeleniumConfigurations>(
                configuration.GetSection("SeleniumConfigurations"))
                    .Configure(seleniumConfigurations);

            var playerController = new PlayerCasoDeUso(seleniumConfigurations);

            playerController.HomePageLoad();

            List<Player> playerList = new List<Player>();

            foreach(var playerId in playerCompareDTO.playerCompare.players) {
                playerController.LoadPage(playerId, playerCompareDTO.playerCompare.month);
                playerList.Add(playerController.GetStatsFromPlayer());
            }
            
            playerController.Exit();

            return playerList;
        }
    }
}