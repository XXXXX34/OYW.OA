using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OYW.OA.DTO.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OYW.OA.Infrastructure.Redis
{
    /// <summary>
    /// Redis 助手
    /// </summary>
    public class RedisHelper
    {

        private readonly RedisOption _redisOption;

        public RedisHelper(IOptions<RedisOption> redisOption)
        {
            _redisOption = redisOption.Value;
        }

        public StackExchange.Redis.ConnectionMultiplexer GetClient()
        {
            StackExchange.Redis.ConfigurationOptions option = new StackExchange.Redis.ConfigurationOptions();
            option.EndPoints.Add(_redisOption.RedisIP, Int32.Parse(_redisOption.RedisPort));
            option.DefaultDatabase = 1;
            StackExchange.Redis.ConnectionMultiplexer conn = StackExchange.Redis.ConnectionMultiplexer.Connect(option);
            return conn;

        }


        #region String 操作

        /// <summary>
        /// 设置 key 并保存字符串（如果 key 已存在，则覆盖值）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool StringSet(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().StringSet(redisKey, redisValue, expiry);
            }
        }

        /// <summary>
        /// 保存多个 Key-value
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public bool StringSet(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs)
        {
            using (var _db = GetClient())
            {
                keyValuePairs =
                keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(AddKeyPrefix(x.Key), x.Value));
                return _db.GetDatabase().StringSet(keyValuePairs.ToArray());
            }
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public string StringGet(string redisKey, TimeSpan? expiry = null)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().StringGet(redisKey);
            }
        }

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool StringSet<T>(string redisKey, T redisValue, TimeSpan? expiry = null)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                var json = Serialize(redisValue);
                return _db.GetDatabase().StringSet(redisKey, json, expiry);
            }
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public T StringGet<T>(string redisKey, TimeSpan? expiry = null)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return Deserialize<T>(_db.GetDatabase().StringGet(redisKey));
            }
        }

        #region async

        /// <summary>
        /// 保存一个字符串值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().StringSetAsync(redisKey, redisValue, expiry);
            }
        }

        /// <summary>
        /// 保存一组字符串值
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValuePairs)
        {
            using (var _db = GetClient())
            {
                keyValuePairs =
                keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(AddKeyPrefix(x.Key), x.Value));
                return await _db.GetDatabase().StringSetAsync(keyValuePairs.ToArray());
            }
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<string> StringGetAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().StringGetAsync(redisKey);
            }
        }

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync<T>(string redisKey, T redisValue, TimeSpan? expiry = null)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                var json = Serialize(redisValue);
                return await _db.GetDatabase().StringSetAsync(redisKey, json, expiry);
            }
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string redisKey, TimeSpan? expiry = null)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return Deserialize<T>(await _db.GetDatabase().StringGetAsync(redisKey));
            }
        }

        #endregion async

        #endregion String 操作

        #region Hash 操作

        /// <summary>
        /// 判断该字段是否存在 hash 中
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public bool HashExists(string redisKey, string hashField)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().HashExists(redisKey, hashField);
            }
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public bool HashDelete(string redisKey, string hashField)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().HashDelete(redisKey, hashField);
            }
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public long HashDelete(string redisKey, IEnumerable<RedisValue> hashField)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().HashDelete(redisKey, hashField.ToArray());
            }
        }

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet(string redisKey, string hashField, string value)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().HashSet(redisKey, hashField, value);
            }
        }

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        public void HashSet(string redisKey, IEnumerable<HashEntry> hashFields)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                _db.GetDatabase().HashSet(redisKey, hashFields.ToArray());
            }
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public RedisValue HashGet(string redisKey, string hashField)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().HashGet(redisKey, hashField);
            }
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public RedisValue[] HashGet(string redisKey, RedisValue[] hashField, string value)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().HashGet(redisKey, hashField);
            }
        }

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public IEnumerable<RedisValue> HashKeys(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().HashKeys(redisKey);
            }
        }

        /// <summary>
        /// 返回 hash 中的所有值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public RedisValue[] HashValues(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().HashValues(redisKey);
            }
        }

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet<T>(string redisKey, string hashField, T value)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                var json = Serialize(value);
                return _db.GetDatabase().HashSet(redisKey, hashField, json);
            }
        }

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public T HashGet<T>(string redisKey, string hashField)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return Deserialize<T>(_db.GetDatabase().HashGet(redisKey, hashField));
            }
        }

        #region async

        /// <summary>
        /// 判断该字段是否存在 hash 中
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string redisKey, string hashField)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().HashExistsAsync(redisKey, hashField);
            }
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string redisKey, string hashField)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().HashDeleteAsync(redisKey, hashField);
            }
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string redisKey, IEnumerable<RedisValue> hashField)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().HashDeleteAsync(redisKey, hashField.ToArray());
            }
        }

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync(string redisKey, string hashField, string value)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().HashSetAsync(redisKey, hashField, value);
            }
        }

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        public async Task HashSetAsync(string redisKey, IEnumerable<HashEntry> hashFields)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                await _db.GetDatabase().HashSetAsync(redisKey, hashFields.ToArray());
            }
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<RedisValue> HashGetAsync(string redisKey, string hashField)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().HashGetAsync(redisKey, hashField);
            }
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RedisValue>> HashGetAsync(string redisKey, RedisValue[] hashField, string value)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().HashGetAsync(redisKey, hashField);
            }
        }

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RedisValue>> HashKeysAsync(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().HashKeysAsync(redisKey);
            }
        }

        /// <summary>
        /// 返回 hash 中的所有值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RedisValue>> HashValuesAsync(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().HashValuesAsync(redisKey);
            }
        }

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<T>(string redisKey, string hashField, T value)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                var json = Serialize(value);
                return await _db.GetDatabase().HashSetAsync(redisKey, hashField, json);
            }
        }

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string redisKey, string hashField)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return Deserialize<T>(await _db.GetDatabase().HashGetAsync(redisKey, hashField));
            }
        }

        #endregion async

        #endregion Hash 操作

        #region List 操作

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public string ListLeftPop(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().ListLeftPop(redisKey);
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public string ListRightPop(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().ListRightPop(redisKey);
            }
        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListRemove(string redisKey, string redisValue)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().ListRemove(redisKey, redisValue);
            }
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListRightPush(string redisKey, string redisValue)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().ListRightPush(redisKey, redisValue);
            }
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListLeftPush(string redisKey, string redisValue)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().ListLeftPush(redisKey, redisValue);
            }
        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public long ListLength(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().ListLength(redisKey);
            }
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public IEnumerable<RedisValue> ListRange(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().ListRange(redisKey);
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return Deserialize<T>(_db.GetDatabase().ListLeftPop(redisKey));
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return Deserialize<T>(_db.GetDatabase().ListRightPop(redisKey));
            }
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListRightPush<T>(string redisKey, T redisValue)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().ListRightPush(redisKey, Serialize(redisValue));
            }
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(string redisKey, T redisValue)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().ListLeftPush(redisKey, Serialize(redisValue));
            }
        }

        #region List-async

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> ListLeftPopAsync(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().ListLeftPopAsync(redisKey);
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> ListRightPopAsync(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().ListRightPopAsync(redisKey);
            }
        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRemoveAsync(string redisKey, string redisValue)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().ListRemoveAsync(redisKey, redisValue);
            }
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string redisKey, string redisValue)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().ListRightPushAsync(redisKey, redisValue);
            }
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync(string redisKey, string redisValue)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().ListLeftPushAsync(redisKey, redisValue);
            }
        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().ListLengthAsync(redisKey);
            }
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RedisValue>> ListRangeAsync(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().ListRangeAsync(redisKey);
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return Deserialize<T>(await _db.GetDatabase().ListLeftPopAsync(redisKey));
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return Deserialize<T>(await _db.GetDatabase().ListRightPopAsync(redisKey));
            }
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string redisKey, T redisValue)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().ListRightPushAsync(redisKey, Serialize(redisValue));
            }
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync<T>(string redisKey, T redisValue)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().ListLeftPushAsync(redisKey, Serialize(redisValue));
            }
        }

        #endregion List-async

        #endregion List 操作

        #region SortedSet 操作

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd(string redisKey, string member, double score)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().SortedSetAdd(redisKey, member, score);
            }
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public IEnumerable<RedisValue> SortedSetRangeByRank(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().SortedSetRangeByRank(redisKey);
            }
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public long SortedSetLength(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().SortedSetLength(redisKey);
            }
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public bool SortedSetLength(string redisKey, string memebr)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().SortedSetRemove(redisKey, memebr);
            }
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd<T>(string redisKey, T member, double score)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                var json = Serialize(member);

                return _db.GetDatabase().SortedSetAdd(redisKey, json, score);
            }
        }

        #region SortedSet-Async

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync(string redisKey, string member, double score)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().SortedSetAddAsync(redisKey, member, score);
            }
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RedisValue>> SortedSetRangeByRankAsync(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().SortedSetRangeByRankAsync(redisKey);
            }
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthAsync(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().SortedSetLengthAsync(redisKey);
            }
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveAsync(string redisKey, string memebr)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().SortedSetRemoveAsync(redisKey, memebr);
            }
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string redisKey, T member, double score)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                var json = Serialize(member);

                return await _db.GetDatabase().SortedSetAddAsync(redisKey, json, score);
            }
        }

        #endregion SortedSet-Async

        #endregion SortedSet 操作

        #region key 操作

        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public bool KeyDelete(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().KeyDelete(redisKey);
            }
        }

        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        public long KeyDelete(IEnumerable<string> redisKeys)
        {
            using (var _db = GetClient())
            {
                var keys = redisKeys.Select(x => (RedisKey)AddKeyPrefix(x));
                return _db.GetDatabase().KeyDelete(keys.ToArray());
            }
        }

        /// <summary>
        /// 校验 Key 是否存在
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public bool KeyExists(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().KeyExists(redisKey);
            }
        }

        /// <summary>
        /// 重命名 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisNewKey"></param>
        /// <returns></returns>
        public bool KeyRename(string redisKey, string redisNewKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().KeyRename(redisKey, redisNewKey);
            }
        }

        /// <summary>
        /// 设置 Key 的时间
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(string redisKey, TimeSpan? expiry)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().KeyExpire(redisKey, expiry);
            }
        }

        #region key-async

        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<bool> KeyDeleteAsync(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().KeyDeleteAsync(redisKey);
            }
        }

        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        public async Task<long> KeyDeleteAsync(IEnumerable<string> redisKeys)
        {
            using (var _db = GetClient())
            {
                var keys = redisKeys.Select(x => (RedisKey)AddKeyPrefix(x));
                return await _db.GetDatabase().KeyDeleteAsync(keys.ToArray());
            }
        }

        /// <summary>
        /// 校验 Key 是否存在
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string redisKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().KeyExistsAsync(redisKey);
            }
        }

        /// <summary>
        /// 重命名 Key
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisNewKey"></param>
        /// <returns></returns>
        public async Task<bool> KeyRenameAsync(string redisKey, string redisNewKey)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().KeyRenameAsync(redisKey, redisNewKey);
            }
        }

        /// <summary>
        /// 设置 Key 的时间
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string redisKey, TimeSpan? expiry)
        {
            using (var _db = GetClient())
            {
                redisKey = AddKeyPrefix(redisKey);
                return await _db.GetDatabase().KeyExpireAsync(redisKey, expiry);
            }
        }

        #endregion key-async

        #endregion key 操作



        #region private method

        /// <summary>
        /// 添加 Key 的前缀
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string AddKeyPrefix(string key)
        {
            return key;
        }



        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                var data = memoryStream.ToArray();
                return data;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] data)
        {
            if (data == null)
                return default(T);

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(data))
            {
                var result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }

        #endregion private method


        #region 泛型
        /// <summary>
        /// 存值并设置过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="t">实体</param>
        /// <param name="ts">过期时间间隔</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Set<T>(string redisKey, T t, TimeSpan ts)
        {
            using (var _db = GetClient())
            {
                var str = JsonConvert.SerializeObject(t);
                redisKey = AddKeyPrefix(redisKey);
                return _db.GetDatabase().StringSet(redisKey, str, ts);
            }
        }

        /// <summary>
        /// 
        /// 根据Key获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public T Get<T>(string key) where T : class
        {
            using (var _db = GetClient())
            {
                var strValue = _db.GetDatabase().StringGet(key);
                return string.IsNullOrEmpty(strValue) ? null : JsonConvert.DeserializeObject<T>(strValue);
            }
        }
        #endregion
    }
}
