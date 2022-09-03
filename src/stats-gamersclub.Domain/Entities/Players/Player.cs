using stats_gamersclub.Domain.Commands;
using stats_gamersclub.Domain.Comum.Results;
using stats_gamersclub.Domain.WebScraper.Interfaces.Players;

namespace stats_gamersclub.Domain.Entities.Player {
    public class Player
    {
        public string Id { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public PlayerStats Stats { get; set; } = new PlayerStats();

        public static Result<List<Player>> Create(IStatsWebScraper statsWebScraper, List<PlayerGC> playersGc) {
            var playersList = new List<Player>();
            foreach (var player in playersGc) {
                playersList.Add(statsWebScraper.ScrapStatsByIdAndMonth(player.Id, player.Month));
            }
            return Result<List<Player>>.Success(playersList);
        }
    }
}
