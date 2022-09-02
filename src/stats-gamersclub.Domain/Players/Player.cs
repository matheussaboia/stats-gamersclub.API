namespace stats_gamersclub.Domain.Player
{
    public class Player
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string Level { get; set; }
        public PlayerStats Stats { get; set; }
    }
}
