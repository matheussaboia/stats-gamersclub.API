namespace stats_gamersclub.API.Dominio
{
    public class Player
    {
        public string nickname { get; set; }
        public string level { get; set; }
        public PlayerStats Stats { get; set; }
    }
}
