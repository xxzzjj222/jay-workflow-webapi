using Jay.Workflow.WebApi.Common.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Interfaces.Migration
{
    /// <summary>
    /// 迁移服务接口
    /// </summary>
    public interface IMigrateService:IScopedDependency
    {
        /// <summary>
        /// 预处理历史版本信息
        /// </summary>
        /// <returns></returns>
        Task<string> PretreatHistoryVersionAsync();

        /// <summary>
        /// 初始化应用
        /// </summary>
        /// <param name="currentDbVersion"></param>
        /// <returns></returns>
        ValueTask InitAppAsync(string currentDbVersion);
    }
}
