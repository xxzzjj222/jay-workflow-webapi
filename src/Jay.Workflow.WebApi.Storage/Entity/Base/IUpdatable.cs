using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Storage.Entity.Base
{
    /// <summary>
    /// 更新
    /// </summary>
    public interface IUpdatable
    {
        void UpdateModification(int userId);
    }
}
