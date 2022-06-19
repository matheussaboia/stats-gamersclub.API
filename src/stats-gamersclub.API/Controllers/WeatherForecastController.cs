using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using stats_gamersclub.API.Models;

namespace stats_gamersclub.API.Controllers
{
    [ApiController]
    [Route("api/stats")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("players/player/{playerId}")]
        public ActionResult<Player> StatsFromPlayer(string playerId)
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

            playerController.LoadPage(playerId);
            var playerStats = playerController.GetStatsFromPlayer();

            playerController.Exit();

            return playerStats;
        }

        [HttpGet]
        [Route("players/player/{playerId1}/player/{playerId2}/compare")]
        public ActionResult<List<Player>> ComparePlayers(string playerId1, string playerId2)
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
            
            List<string> playerIds = new List<string>();
            playerIds.Add(playerId1);
            playerIds.Add(playerId2);

            foreach(var playerId in playerIds) {
                playerController.LoadPage(playerId);
                playerList.Add(playerController.GetStatsFromPlayer());
            }
            
            playerController.Exit();

            return playerList;
        }
    }
}