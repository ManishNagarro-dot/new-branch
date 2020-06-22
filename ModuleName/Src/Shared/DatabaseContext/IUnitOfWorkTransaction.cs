using System;

namespace gov.seeker.moduleName.Shared.DatabaseContext
{
    /// <summary>
    /// Interface IUnitOfWorkTransaction
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWorkTransaction : IDisposable
    {
        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        void Rollback();
    }
}