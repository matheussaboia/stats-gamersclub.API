using MediatR;
using stats_gamersclub.Domain.Commands;
using stats_gamersclub.Domain.Comum.Entidades;
using stats_gamersclub.Domain.Comum.Results;
using stats_gamersclub.Domain.Player;
using stats_gamersclub.Infra.Repository;

namespace stats_gamersclub.Application.Handlers {
    public class GetPlayersStatsHandler : IRequestHandler<GetPlayersStatsCommand, IResult> {
        public async Task<IResult> Handle(GetPlayersStatsCommand request, CancellationToken cancellationToken) {

            var haveSamePlayers = request.Players.GroupBy(x => x.Id).Any(g => g.Count() > 1);
            if (haveSamePlayers)
                return Result<string>.UnprocessableEntity("Não é possível utilizar GC Id repetido.");

            var statsRepository = new StatsRepository();
            var playerList = new List<Player>();

            foreach (var player in request.Players) {
                statsRepository.LoadPlayerPage(player.Id, player.Month);
                playerList.Add(statsRepository.GetStatsFromPlayer());
            }

            statsRepository.Exit();

            return Result<List<Player>>.Success(playerList);
        }
    }
}
