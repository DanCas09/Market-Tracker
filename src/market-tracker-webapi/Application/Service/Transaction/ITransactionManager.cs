using market_tracker_webapi.Application.Utils;

namespace market_tracker_webapi.Application.Service.Transaction;

public interface ITransactionManager
{
    Task<Either<TError, T>> ExecuteAsync<TError, T>(Func<Task<Either<TError, T>>> action);
}
