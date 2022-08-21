using MediatR;
using stats_gamersclub.Domain.Comum.Results;

namespace stats_gamersclub.Domain.Commands {
    public class GetPlayersStatsToCompareCommand : IRequest<IResult> {
        public PlayerCompare PlayerCompare { get; set; }
    }

    public class PlayerCompare {
        public List<PlayerGC> Players { get; set; }
    }

    public class PlayerGC {
        public string Id { get; set; }
        public string Month { get; set; }
    }
}
