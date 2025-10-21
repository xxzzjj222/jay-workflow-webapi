using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Enums
{
    /// <summary>
    /// 按钮类型
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 查看
        /// </summary>
        [Description("查看")]
        View = 1,
        /// <summary>
        ///  新增
        /// </summary>
        [Description("新增")]
        Add = 2,
        /// <summary>
        ///    编辑
        /// </summary>
        [Description("编辑")]
        Edit = 3,
        /// <summary>
        ///     删除
        /// </summary>
        [Description("删除")]
        Delete = 4,
        /// <summary>
        ///    打印
        /// </summary>
        [Description("打印")]
        Print = 5,
        /// <summary>
        ///    审核
        /// </summary>
        [Description("审核")]
        Check = 6,
        /// <summary>
        ///     作废
        /// </summary>
        [Description("作废")]
        Cancle = 7,
        /// <summary>
        ///    结束
        /// </summary>
        [Description("结束")]
        Finish = 8,
        /// <summary>
        ///    扩展
        /// </summary>
        [Description("扩展")]
        Extend = 9
    }
}
