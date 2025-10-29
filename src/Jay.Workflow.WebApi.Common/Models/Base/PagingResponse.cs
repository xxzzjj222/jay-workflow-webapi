using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Models.Base
{
    /// <summary>
    /// 分页响应dto
    /// </summary>
    public class PagingResponse<T> : ResponseDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PagingResponse()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="count"></param>
        /// <param name="datas"></param>
        public PagingResponse(int count, List<T> datas)
        {
            Count = count;

            Datas = datas;
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        [JsonProperty("datas")]
        public List<T> Datas { get; set; } = new List<T>();
    }
}
