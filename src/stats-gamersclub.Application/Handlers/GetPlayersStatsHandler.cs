using MediatR;
using stats_gamersclub.Domain.Commands;
using stats_gamersclub.Domain.Comum.Results;
using stats_gamersclub.Domain.Entities.Player;
using stats_gamersclub.Infra.WebScraper;

namespace stats_gamersclub.Application.Handlers {
    public class GetPlayersStatsHandler : IRequestHandler<GetPlayersStatsCommand, IResult> {

        public async Task<IResult> Handle(GetPlayersStatsCommand request, CancellationToken cancellationToken) {

            var haveSamePlayers = request.Players.GroupBy(x => x.Id).Any(g => g.Count() > 1);
            if (haveSamePlayers)
                return Result<string>.UnprocessableEntity("Não é possível utilizar GC Id's iguais.");

            var statsWebScraper = new StatsWebScraper();
            var playerList = Player.Create(statsWebScraper, request.Players);
            statsWebScraper.Exit();

            return Result<List<Player>>.Success(playerList.GetValue()!);
        }
    }
}
