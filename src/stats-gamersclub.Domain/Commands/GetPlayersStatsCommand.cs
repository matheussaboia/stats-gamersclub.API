using MediatR;
using stats_gamersclub.Domain.Comum.Results;

namespace stats_gamersclub.Domain.Commands {
    public class GetPlayersStatsCommand : IRequest<IResult> {
        public List<PlayerGC> Players { get; set; } = new List<PlayerGC>();
    }

    public class PlayerGC {
        public string Id { get; set; } = string.Empty;
        public string Month { get; set; } = string.Empty;
    }
}
