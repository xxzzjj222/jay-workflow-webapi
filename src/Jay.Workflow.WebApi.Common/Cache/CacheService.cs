using Jay.Workflow.WebApi.Common.Interface.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Common.Cache
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    public class CacheService : ICacheService
    {
        private readonly IDatabase _redisDb;

        /// <summary>
        /// 构造函数（注入 Redis 连接）
        /// </summary>
        /// <param name="connectionMultiplexer">Redis 连接复用器（单例）</param>
        /// <param name="dbNumber">数据库编号（默认 0）</param>
        public CacheService(IConnectionMultiplexer connectionMultiplexer, int dbNumber = 0)
        {
            if (connectionMultiplexer == null)
                throw new ArgumentNullException(nameof(connectionMultiplexer), "Redis 连接复用器不能为空");

            _redisDb = connectionMultiplexer.GetDatabase(dbNumber);
        }

        #region 自定义
        public async Task<bool> SetAsync<T>(string key,T value,TimeSpan? expiry = null)
        {
            string strValue=JsonConvert.SerializeObject(value);
            return await _redisDb.StringSetAsync(key, strValue, expiry).ConfigureAwait(false);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            string strValue = await _redisDb.StringGetAsync(key).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(strValue);
        }

        public async Task<bool> DeleteAsync(params string[] keys)
        {
            Parallel.ForEach(keys, async key =>
            {
                await _redisDb.KeyDeleteAsync(key).ConfigureAwait(false);
            });

            return true;
        }

        public async Task<long> IncrementAsync(string key,long step,TimeSpan? expiry = null)
        {
            var newValue = await _redisDb.StringIncrementAsync(key, step).ConfigureAwait(false);
            await _redisDb.KeyExpireAsync(key, expiry).ConfigureAwait(false);
            return newValue;
        }
        #endregion

        #region 字符串操作
        public bool StringSet(string key, string value, TimeSpan? expiry = null)
        {
            return _redisDb.StringSet(key, value, expiry);
        }

        public async Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = null)
        {
            return await _redisDb.StringSetAsync(key, value, expiry).ConfigureAwait(false);
        }

        public string StringGet(string key)
        {
            return _redisDb.StringGet(key);
        }

        public async Task<string> StringGetAsync(string key)
        {
            return await _redisDb.StringGetAsync(key).ConfigureAwait(false);
        }
        #endregion

        #region 哈希操作
        public bool HashSet(string hashKey, string field, string value)
        {
            return _redisDb.HashSet(hashKey, field, value);
        }

        public async Task<bool> HashSetAsync(string hashKey, string field, string value)
        {
            return await _redisDb.HashSetAsync(hashKey, field, value).ConfigureAwait(false);
        }

        public string HashGet(string hashKey, string field)
        {
            return _redisDb.HashGet(hashKey, field);
        }

        public async Task<string> HashGetAsync(string hashKey, string field)
        {
            return await _redisDb.HashGetAsync(hashKey, field).ConfigureAwait(false);
        }

        public Dictionary<string, string> HashGetAll(string hashKey)
        {
            var hashEntries = _redisDb.HashGetAll(hashKey);
            return hashEntries.ToDictionary(
                entry => entry.Name.ToString(),
                entry => entry.Value.ToString()
            );
        }

        public async Task<Dictionary<string, string>> HashGetAllAsync(string hashKey)
        {
            var hashEntries = await _redisDb.HashGetAllAsync(hashKey).ConfigureAwait(false);
            return hashEntries.ToDictionary(
                entry => entry.Name.ToString(),
                entry => entry.Value.ToString()
            );
        }
        #endregion

        #region 通用操作
        public bool KeyDelete(string key)
        {
            return _redisDb.KeyDelete(key);
        }

        public async Task<bool> KeyDeleteAsync(string key)
        {
            return await _redisDb.KeyDeleteAsync(key).ConfigureAwait(false);
        }

        public bool KeyExists(string key)
        {
            return _redisDb.KeyExists(key);
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _redisDb.KeyExistsAsync(key).ConfigureAwait(false);
        }

        public bool KeyExpire(string key, TimeSpan expiry)
        {
            return _redisDb.KeyExpire(key, expiry);
        }

        public async Task<bool> KeyExpireAsync(string key, TimeSpan expiry)
        {
            return await _redisDb.KeyExpireAsync(key, expiry).ConfigureAwait(false);
        }
        #endregion
    }
}
