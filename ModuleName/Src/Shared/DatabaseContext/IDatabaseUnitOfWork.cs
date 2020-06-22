using Microsoft.EntityFrameworkCore.Storage;
using gov.seeker.moduleName.Entities;
using System.Data;
using gov.seeker.moduleName.Shared.Repositories;
using System.Threading.Tasks;

namespace gov.seeker.moduleName.Shared.DatabaseContext
{
    /// <summary>
    /// Represents a database unit of work
    /// </summary>
    public interface IDatabaseUnitOfWork
    {
        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns>System.Int32.</returns>
        Task<int> Commit();

        /// <summary>
        /// Generate repository based on Entity
        /// </summary>
        /// <typeparam name="TEntity">Entity for repository</typeparam>
        /// <returns>Typed repository.</returns>
        IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="dbIsolationLevel">The database isolation level.</param>
        /// <returns></returns>
        IUnitOfWorkTransaction BeginTransaction(IsolationLevel dbIsolationLevel = IsolationLevel.ReadUncommitted);

        /// <summary>
        /// Database Object
        /// </summary>
        IExecutionStrategy ExecutionStrategy { get; }
    }
}

