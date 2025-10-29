using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        /// <summary>
        /// 修改人
        /// </summary>
        public int? Modifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedTime { get; set; }

        void UpdateModification(int userId);
    }
}
