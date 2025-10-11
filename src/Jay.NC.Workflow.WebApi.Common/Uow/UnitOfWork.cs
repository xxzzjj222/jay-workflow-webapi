using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Common.Uow
{
    /// <summary>
    /// unitofwork
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private bool _disposed = false;

        protected Lazy<DbContext> _dbContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWork(T dbContext)
        {
            _dbContext = new Lazy<DbContext>(() => dbContext);
        }

        /// <summary>
        /// 创建事务
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Value.Database.BeginTransactionAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        public void CommitTransaction()
        {
            _dbContext.Value.Database.CommitTransaction();
        }

        /// <summary>
        /// 提交SaveChangesAsync
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await _dbContext.Value.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 回滚
        /// </summary>
        /// <returns></returns>
        public void Rollback()
        {
            _dbContext.Value.ChangeTracker.Entries().ToList().ForEach(async r => await r.ReloadAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <returns></returns>
        public void RollbackTransaction()
        {
            _dbContext.Value.Database.RollbackTransaction();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Value?.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
