namespace stats_gamersclub.API.Models
{
    public class PlayerCompareDTO
    {
        public PlayerCompare playerCompare { get; set; }
    }

    public class PlayerCompare
    {
        public List<string> playersIds { get; set; }
        public string monthStats { get; set; }
    }
}
