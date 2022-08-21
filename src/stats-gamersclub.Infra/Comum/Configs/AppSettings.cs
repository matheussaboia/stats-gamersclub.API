using stats_gamersclub.Domain.Comum.Options;

namespace stats_gamersclub.Infra.Comum.Configs {
    public class AppSettings {
        public static GamersClubOptions? GamersClub { get; private set; }

        public static void SetarOpcoes(GamersClubOptions? gamersClubOptions) {
            GamersClub = gamersClubOptions;
        }
    }
}
