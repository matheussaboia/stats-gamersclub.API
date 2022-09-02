using MediatR;
using stats_gamersclub.Domain.Comum.Results;

namespace stats_gamersclub.Domain.Commands {
    public class GetMonthFromPlayerStatsCommand : IRequest<IResult> {
        public List<string> Players { get; set; } = default!;
    }
}
