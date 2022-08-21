using MediatR;
using stats_gamersclub.Domain.Comum.Results;

namespace stats_gamersclub.Domain.Commands {
    public class GetPlayersStatsToCompareCommand : IRequest<IResult> {
        public PlayerCompare playerCompare { get; set; }
    }

    public class PlayerCompare {
        public List<string> players { get; set; }
        public string month { get; set; }
    }
}
