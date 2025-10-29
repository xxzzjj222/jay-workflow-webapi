using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Models.Base
{
    /// <summary>
    /// 分页请求dto
    /// </summary>
    public class PagingRequest : RequestDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 分页数量
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 关键词
        /// </summary>
        [JsonProperty("keyword")]
        public string? Keyword { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [JsonProperty("sorts")]
        public List<Sort>? Sorts { get; set; }
    }

    /// <summary>
    /// 排序
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否降序排序
        /// </summary>
        public bool IsDesc { get; set; }
    }
}
