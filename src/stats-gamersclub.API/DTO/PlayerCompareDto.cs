namespace stats_gamersclub.API.DTO
{
    public class PlayerCompareDto
    {
        public PlayerCompare playerCompare { get; set; }
    }

    public class PlayerCompare
    {
        public List<string> players { get; set; }
        public string month { get; set; }
    }
}
