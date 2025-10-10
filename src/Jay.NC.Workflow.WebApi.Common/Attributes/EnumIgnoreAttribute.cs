using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Common.Attributes
{
    /// <summary>
    /// 是否忽略枚举项
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumIgnoreAttribute:Attribute
    {
    }
}
