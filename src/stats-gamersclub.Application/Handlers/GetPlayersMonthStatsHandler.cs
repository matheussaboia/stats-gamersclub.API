using MediatR;
using stats_gamersclub.Domain.Commands;
using stats_gamersclub.Domain.Comum.Results;
using stats_gamersclub.Domain.Players;
using stats_gamersclub.Infra.Repository;

namespace stats_gamersclub.Application.Handlers {
    public class GetPlayersMonthStatsHandler : IRequestHandler<GetMonthFromPlayerStatsCommand, IResult> {
        public async Task<IResult> Handle(GetMonthFromPlayerStatsCommand request, CancellationToken cancellationToken) {

            var haveSamePlayers = request.Players.GroupBy(x => x).Any(g => g.Count() > 1);
            if (haveSamePlayers)
                return Result<string>.UnprocessableEntity("Não é possível utilizar GC Id repetido.");

            var statsRepository = new StatsRepository();
            var listMonths = new List<PlayerMonth>();

            foreach (var player in request.Players) {
                listMonths.Add(new PlayerMonth { PlayerId = player, Month = statsRepository.LoadPlayerMonth(player) });
            }
            
            statsRepository.Exit();

            return Result<List<PlayerMonth>>.Success(listMonths);
        }
    }
}
