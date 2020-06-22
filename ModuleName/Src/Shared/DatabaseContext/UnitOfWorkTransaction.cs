
namespace gov.seeker.moduleName.Shared.DatabaseContext
{
    using Microsoft.EntityFrameworkCore.Storage;
    using System;

    /// <summary>
    /// UnitOfWorkTransaction class.
    /// </summary>
    public class UnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        /// <summary>
        /// The transaction object.
        /// </summary>
        protected IDbContextTransaction Transaction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkTransaction"/> class.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        public UnitOfWorkTransaction(IDbContextTransaction transaction)
        {
            this.Transaction = transaction;
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Commit();
            }
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Rollback();
            }
        }

        #region IDisposable and the Dispose pattern

        /// <summary>
        /// 
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDisposing"></param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (_disposed) return;
            if (isDisposing)
            {
                if (this.Transaction != null)
                {
                    this.Transaction.Dispose();
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
