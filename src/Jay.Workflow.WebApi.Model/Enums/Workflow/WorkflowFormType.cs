using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Model.Enums.Workflow
{
    //
    // 摘要:
    //     流程表单类型
    public enum WorkflowFormType
    {
        //
        // 摘要:
        //     自定义表单
        [Description("自定义表单")]
        Custom,
        //
        // 摘要:
        //     系统表单
        [Description("系统表单")]
        System
    }
}
