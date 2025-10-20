using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Storage.Entity.Base
{
    /// <summary>
    /// 删除
    /// </summary>
    public interface IDeletable
    {
        void UpdateDeletation(int userId);
    }
}
