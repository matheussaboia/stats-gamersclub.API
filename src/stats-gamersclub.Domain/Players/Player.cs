namespace stats_gamersclub.Domain.Player
{
    public class Player
    {
        public string nickname { get; set; }
        public string level { get; set; }
        public PlayerStats Stats { get; set; }
    }
}
