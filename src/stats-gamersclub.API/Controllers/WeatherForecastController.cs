using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using stats_gamersclub.API.Models;

namespace stats_gamersclub.API.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
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

            var playerController = new PlayerController(seleniumConfigurations);

            playerController.HomePageLoad();

            playerController.LoadPage(playerId, month);
            Player player = playerController.GetStatsFromPlayer();

            playerController.Exit();

            return player;
        }

        [HttpPost]
        [Route("/api/stats/players/compare")]
        public ActionResult<List<Player>> StatsFromPlayers([FromBody] PlayerCompareDTO playerCompareDTO)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            var configuration = builder.Build();

            var seleniumConfigurations = new SeleniumConfigurations();
                
            new ConfigureFromConfigurationOptions<SeleniumConfigurations>(
                configuration.GetSection("SeleniumConfigurations"))
                    .Configure(seleniumConfigurations);

            var playerController = new PlayerController(seleniumConfigurations);

            playerController.HomePageLoad();

            List<Player> playerList = new List<Player>();

            foreach(var playerId in playerCompareDTO.playerCompare.playersIds) {
                playerController.LoadPage(playerId, playerCompareDTO.playerCompare.monthStats);
                playerList.Add(playerController.GetStatsFromPlayer());
            }
            
            playerController.Exit();

            return playerList;
        }
    }
}