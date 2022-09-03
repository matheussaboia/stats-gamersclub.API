using stats_gamersclub.Domain.Entities.Player;

namespace stats_gamersclub.Domain.WebScraper.Interfaces.Players {
    public interface IStatsWebScraper {
        public List<string> ScrapMonthsById(string id);
        public Player ScrapStatsByIdAndMonth(string id, string monthStats);
    }
}
