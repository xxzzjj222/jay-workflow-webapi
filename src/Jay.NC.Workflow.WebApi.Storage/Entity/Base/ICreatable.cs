using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Storage.Entity.Base
{
    /// <summary>
    /// 创建
    /// </summary>
    public interface ICreatable
    {
        void UpdateCreation(int userId);
    }
}
