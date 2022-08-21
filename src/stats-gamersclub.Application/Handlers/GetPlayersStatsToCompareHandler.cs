﻿using MediatR;
using stats_gamersclub.Domain.Commands;
using stats_gamersclub.Domain.Comum.Results;
using stats_gamersclub.Domain.Player;
using stats_gamersclub.Infra.Repository;

namespace stats_gamersclub.Application.Handlers {
    public class GetPlayersStatsToCompareHandler : IRequestHandler<GetPlayersStatsToCompareCommand, IResult> {
        public async Task<IResult> Handle(GetPlayersStatsToCompareCommand request, CancellationToken cancellationToken) {

            var statsRepository = new StatsRepository();

            var playerList = new List<Player>();

            statsRepository.LoadHomePage();

            foreach (var playerId in request.playerCompare.players) {
                statsRepository.LoadPlayerPage(playerId, request.playerCompare.month);
                playerList.Add(statsRepository.GetStatsFromPlayer());
            }

            statsRepository.Exit();

            return Result<List<Player>>.Success(playerList);
        }
    }
}