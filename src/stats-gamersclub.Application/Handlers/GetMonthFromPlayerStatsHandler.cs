using MediatR;
using stats_gamersclub.Domain.Commands;
using stats_gamersclub.Domain.Comum.Results;
using stats_gamersclub.Domain.Players;
using stats_gamersclub.Infra.Repository;

namespace stats_gamersclub.Application.Handlers {
    public class GetMonthFromPlayerStatsHandler : IRequestHandler<GetMonthFromPlayerStats, IResult> {
        public async Task<IResult> Handle(GetMonthFromPlayerStats request, CancellationToken cancellationToken) {

            var statsRepository = new StatsRepository();
            var listMonths = new List<PlayerMonth>();

            statsRepository.LoadHomePage();

            foreach (var playerId in request.PlayerIds) {
                listMonths.Add(new PlayerMonth { PlayerId = playerId, Month = statsRepository.LoadPlayerMonth(playerId) });
            }
            
            statsRepository.Exit();

            return Result<List<PlayerMonth>>.Success(listMonths);
        }
    }
}
