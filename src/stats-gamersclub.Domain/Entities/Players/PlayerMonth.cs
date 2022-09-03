using stats_gamersclub.Domain.Comum.Results;
using stats_gamersclub.Domain.WebScraper.Interfaces.Players;

namespace stats_gamersclub.Domain.Entities.Players {
    public class PlayerMonth {
        public string PlayerId { get; set; } = string.Empty;
        public List<string> Month { get; set; } = default!;

        public static Result<List<PlayerMonth>> Create(IStatsWebScraper statsWebScraper, List<string> playersIds) {
            var monthsList = new List<PlayerMonth>();
            foreach (var playerId in playersIds) {
                monthsList.Add(new PlayerMonth { PlayerId = playerId, Month = statsWebScraper.ScrapMonthsById(playerId) });
            }
            return Result<List<PlayerMonth>>.Success(monthsList);
        }
    }
}
