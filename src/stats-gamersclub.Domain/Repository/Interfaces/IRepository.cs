namespace stats_gamersclub.Domain.Repository.Interfaces {
    public interface IRepository<TIRaizAgregacao> where TIRaizAgregacao : class {
        Task<TIRaizAgregacao?> ObterAsync(ISpecification<TIRaizAgregacao> especificacao, bool usarTracking = true);
        Task<IEnumerable<TIRaizAgregacao>> ListarAsync(ISpecification<TIRaizAgregacao> especificacao, bool usarTracking = true);
        Task<IEnumerable<TIRaizAgregacao>> ListarAsync(bool usarTracking = true);
        Task AdicionarAsync(TIRaizAgregacao entidade);
        Task AtualizarAsync(TIRaizAgregacao entidade);
        Task RemoverAsync(TIRaizAgregacao entidade);
    }
}
