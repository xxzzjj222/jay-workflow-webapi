using Jay.Workflow.WebApi.Common.Enums;
using Jay.Workflow.WebApi.Common.Interface;
using Jay.Workflow.WebApi.Storage.Entity;
using Jay.Workflow.WebApi.Storage.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Storage.Context
{
    public class WorkflowDbContext:DbContext
    {
        private readonly IHttpContextUtility _httpContextUtility;

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        //public WorkflowDbContext(IHttpContextUtility httpContextUtility)
        //{
        //    _httpContextUtility = httpContextUtility;
        //}

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
        public DbSet<Resource> Resource { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserDepartment> UserDepartment { get; set; }
        public DbSet<RoleResource> RoleResource { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemConfig>().HasData(
                new SystemConfig()
                {
                    Id = 1,
                    ConfigKey = "IsEnableSwagger",
                    ConfigName = "是否启用Swagger",
                    ConfigValue = "true",
                    ExtData = "",
                    SortNo = 1,
                    Status = EnableDisableEnum.Enable,
                    Remark = null,
                    ConfigType = ConfigTypeEnum.Env
                });

            var deletableTypes = modelBuilder.Model.GetEntityTypes().Where(t => typeof(IDeletable).IsAssignableFrom(t.ClrType)).Select(t => t.ClrType).ToList();
            foreach(var type in deletableTypes)
            {
                var filterExpression = CreateSoftDeleteFilter(type);
                modelBuilder.Entity(type).HasQueryFilter(filterExpression);
            }
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

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        private LambdaExpression CreateSoftDeleteFilter(Type entityType)
        {
            var parameter = Expression.Parameter(entityType);
            var property = Expression.Property(parameter, nameof(IDeletable.IsDeleted));
            var notDeleted = Expression.Not(property);
            return Expression.Lambda(notDeleted, parameter);
        }
    }
}
