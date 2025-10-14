using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Storage.Entity.Base
{
    public class BaseEntity : ICreatable, IUpdatable, IDeletable
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("creator")]
        public int Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("created_time")]
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("modifier")]
        public int? Modifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("modified_time")]
        public DateTime? ModifiedTime { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        [Column("deleter")]
        public int? Deleter { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("deleted_time")]
        public DateTime? DeletedTime { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="userId"></param>
        public void UpdateCreation(int userId)
        {
            Creator = userId;
            CreatedTime = DateTime.Now;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="userId"></param>
        public void UpdateModification(int userId)
        {
            Modifier = userId;
            ModifiedTime = DateTime.Now;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userId"></param>
        public void UpdateDeletation(int userId)
        {
            Deleter = userId;
            DeletedTime = DateTime.Now;
        }
    }
}
