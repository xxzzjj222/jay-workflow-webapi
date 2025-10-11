using Jay.NC.Workflow.WebApi.Common.Interface;
using Jay.NC.Workflow.WebApi.Storage.Entity;
using Jay.NC.Workflow.WebApi.Storage.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Storage.Context
{
    public class WorkflowDbContext:DbContext
    {
        private readonly IHttpContextUtility _httpContextUtility;

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowDbContext(IHttpContextUtility httpContextUtility)
        {
            _httpContextUtility = httpContextUtility;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">参数</param>
        /// <param name="httpContextUtility"></param>
        public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options, IHttpContextUtility httpContextUtility)
            : base(options)
        {
            _httpContextUtility = httpContextUtility;
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<SystemConfig> SystemConfig { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(e =>
            {
                e.Property(e => e.Id).HasDefaultValueSql("nextval('config.data_source_id_seq'::regclass)");
            });
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            int currentUserId;

            try
            {
                currentUserId = _httpContextUtility.GetUserInfo()?.UserId ?? 100000;
            }
            catch
            {
                //异常时保存为100000
                currentUserId = 100000;
            }

            foreach (var entry in ChangeTracker.Entries<ICreatable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.UpdateCreation(currentUserId);
                }
            }

            foreach (var entry in ChangeTracker.Entries<IUpdatable>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdateModification(currentUserId);
                }
            }

            foreach (var entry in ChangeTracker.Entries<IDeletable>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.UpdateDeletation(currentUserId);
                }
            }
        }
    }
}
