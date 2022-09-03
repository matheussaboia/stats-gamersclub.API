using System.Linq.Expressions;

namespace stats_gamersclub.Domain.Repository.Interfaces {
    public interface ISpecification<T> {
        Expression<Func<T, bool>> Criterio { get; }
        List<Expression<Func<T, object>>> Inclusoes { get; }
        IList<string> SubInclusoes { get; }
        List<Expression<Func<T, object>>> InclusoesFilter { get; }
    }
}
