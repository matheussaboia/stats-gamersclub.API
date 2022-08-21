using MediatR;
using stats_gamersclub.Domain.Commands;
using stats_gamersclub.Domain.Comum.Results;
using stats_gamersclub.Domain.Player;
using stats_gamersclub.Infra.Repository;

namespace stats_gamersclub.Application.Handlers {
    public class GetStatsHandler : IRequestHandler<GetPlayerStatsCommand, IResult>
    {
        public async Task<IResult> Handle(GetPlayerStatsCommand request, CancellationToken cancellationToken) {

            var statsRepository = new StatsRepository();

            statsRepository.LoadHomePage();
            statsRepository.LoadPlayerPage(request.PlayerId, request.Month);
            Player player = statsRepository.GetStatsFromPlayer();

            statsRepository.Exit();

            return Result<Player>.Success(player);
        }
    }
}
