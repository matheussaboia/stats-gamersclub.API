using stats_gamersclub.Domain.Entities.Player;

namespace stats_gamersclub.Domain.Repository.Interfaces.Players {
    public interface IPlayerRepository : IRepository<Player> {
        Task<bool> Scrap(ISpecification<Player> especificacao);
    }
}
