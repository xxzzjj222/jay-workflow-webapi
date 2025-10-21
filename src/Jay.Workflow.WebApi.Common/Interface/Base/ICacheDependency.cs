using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Interface.Base
{
    /// <summary>
    /// 声明 Cache依赖
    /// </summary>
    public interface ICacheDependency
    {
        /// <summary>
        /// 刷新或增加缓存
        /// </summary>
        /// <returns></returns>
        Task<bool> RefreshCacheAsync();
    }
}
