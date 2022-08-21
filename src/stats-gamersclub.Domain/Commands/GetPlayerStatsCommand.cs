using MediatR;
using stats_gamersclub.Domain.Comum.Results;

namespace stats_gamersclub.Domain.Commands
{
    public class GetPlayerStatsCommand : IRequest<IResult> {
        public string PlayerId { get; set; }
        public string Month { get; set; }
    }
}
