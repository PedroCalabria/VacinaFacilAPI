using System.Data;

namespace VacinaFacil.Repository.Interface
{
    public interface ITransactionManager
    {
        Task BeginTransactionAsync(IsolationLevel isolationLevel);
        Task CommitTransactionAsync();
        Task RollbackTransactionsAsync();
    }
}
