using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        /// <summary>
        /// IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        public int? Deleter { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletedTime { get; set; }

        void UpdateDeletation(int userId);
    }
}
