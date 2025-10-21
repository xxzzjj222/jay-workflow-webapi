using Jay.Workflow.WebApi.Common.DynamicInterceptor.Attributes;
using Jay.Workflow.WebApi.Common.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Cache
{
    /// <summary>
    /// 缓存 操作服务接口
    /// </summary>
    public interface ICacheService:IScopedDependency
    {
        #region 自定义
        Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null);

        Task<T> GetAsync<T>(string key);

        Task<bool> DeleteAsync(params string[] keys);
        #endregion

        #region 字符串操作
        /// <summary>
        /// 设置字符串键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间（null 表示永久）</param>
        /// <returns>是否成功</returns>
        [DiagnoseInterceptor]
        bool StringSet(string key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// 异步设置字符串键值对
        /// </summary>
        [DiagnoseInterceptor]
        Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// 获取字符串值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值（不存在返回 null）</returns>
        [DiagnoseInterceptor]
        string StringGet(string key);

        /// <summary>
        /// 异步获取字符串值
        /// </summary>
        [DiagnoseInterceptor]
        Task<string> StringGetAsync(string key);
        #endregion

        #region 哈希操作
        /// <summary>
        /// 设置哈希字段值
        /// </summary>
        /// <param name="hashKey">哈希表键</param>
        /// <param name="field">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns>是否成功</returns>
        bool HashSet(string hashKey, string field, string value);

        /// <summary>
        /// 异步设置哈希字段值
        /// </summary>
        Task<bool> HashSetAsync(string hashKey, string field, string value);

        /// <summary>
        /// 获取哈希字段值
        /// </summary>
        /// <param name="hashKey">哈希表键</param>
        /// <param name="field">字段名</param>
        /// <returns>字段值（不存在返回 null）</returns>
        string HashGet(string hashKey, string field);

        /// <summary>
        /// 异步获取哈希字段值
        /// </summary>
        Task<string> HashGetAsync(string hashKey, string field);

        /// <summary>
        /// 获取哈希表所有字段和值
        /// </summary>
        Dictionary<string, string> HashGetAll(string hashKey);

        /// <summary>
        /// 异步获取哈希表所有字段和值
        /// </summary>
        Task<Dictionary<string, string>> HashGetAllAsync(string hashKey);
        #endregion

        #region 通用操作
        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否删除成功</returns>
        bool KeyDelete(string key);

        /// <summary>
        /// 异步删除键
        /// </summary>
        Task<bool> KeyDeleteAsync(string key);

        /// <summary>
        /// 检查键是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否存在</returns>
        bool KeyExists(string key);

        /// <summary>
        /// 异步检查键是否存在
        /// </summary>
        Task<bool> KeyExistsAsync(string key);

        /// <summary>
        /// 设置键过期时间
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>是否成功</returns>
        bool KeyExpire(string key, TimeSpan expiry);

        /// <summary>
        /// 异步设置键过期时间
        /// </summary>
        Task<bool> KeyExpireAsync(string key, TimeSpan expiry);
        #endregion
    }
}
