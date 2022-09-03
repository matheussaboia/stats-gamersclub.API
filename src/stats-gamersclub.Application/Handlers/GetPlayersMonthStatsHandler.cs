using MediatR;
using stats_gamersclub.Domain.Commands;
using stats_gamersclub.Domain.Comum.Results;
using stats_gamersclub.Domain.Entities.Players;
using stats_gamersclub.Infra.WebScraper;

namespace stats_gamersclub.Application.Handlers {
    public class GetPlayersMonthStatsHandler : IRequestHandler<GetMonthFromPlayerStatsCommand, IResult> {
        public async Task<IResult> Handle(GetMonthFromPlayerStatsCommand request, CancellationToken cancellationToken) {

            var haveSamePlayers = request.Players.GroupBy(x => x).Any(g => g.Count() > 1);
            if (haveSamePlayers)
                return Result<string>.UnprocessableEntity("Não é possível utilizar GC Id's iguais.");

            var statsWebScraper = new StatsWebScraper();
            var listMonths = PlayerMonth.Create(statsWebScraper, request.Players);
            statsWebScraper.Exit();

            return Result<List<PlayerMonth>>.Success(listMonths.GetValue()!);
        }
    }
}
