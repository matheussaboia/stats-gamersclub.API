using MediatR;
using stats_gamersclub.Domain.Comum.Results;

namespace stats_gamersclub.Domain.Commands {
    public class GetMonthFromPlayerStats : IRequest<IResult> {
        public List<string> PlayerIds { get; set; } = default!;
    }
}
