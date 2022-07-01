namespace stats_gamersclub.API.DTO
{
    public class PlayerCompareDto
    {
        public PlayerCompare playerCompare { get; set; }
    }

    public class PlayerCompare
    {
        public List<string> playersIds { get; set; }
        public string monthStats { get; set; }
    }
}
