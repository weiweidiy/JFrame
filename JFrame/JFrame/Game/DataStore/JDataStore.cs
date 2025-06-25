using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JFramework
{
    public class JDataStore : IGameDataStore, IDisposable
    {
        private readonly Dictionary<string, object> _memoryCache = new Dictionary<string, object>();
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;
        private readonly IWriter _writer;
        private readonly IReader _reader;
        private readonly IDelete _deleter;
        private bool _disposed;

        public JDataStore(ISerializer serializer,
                         IDeserializer deserializer,
                         IWriter writer,
                         IReader reader,
                         IDelete deleter)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _deleter = deleter ?? throw new ArgumentNullException(nameof(deleter));
        }

        public async Task<bool> ExistsAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or whitespace", nameof(key));

            // 先检查内存缓存
            if (_memoryCache.ContainsKey(key))
                return true;

            // 然后检查持久化存储
            return await _reader.ExistsAsync(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or whitespace", nameof(key));

            // 先检查内存缓存
            if (_memoryCache.TryGetValue(key, out var cachedValue))
            {
                if (cachedValue is T typedValue)
                    return typedValue;

                // 如果类型不匹配，从存储重新加载
            }

            // 从持久化存储读取
            var data = await _reader.ReadAsync<T>(key, _deserializer);
            if (data == null)
                return default;

            // 更新内存缓存
            _memoryCache[key] = data;

            return data;
        }

        public async Task SaveAsync<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or whitespace", nameof(key));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            // 序列化数据
            var serializedData = _serializer.Serialize(value);

            // 写入持久化存储
            await _writer.WriteAsync(key, serializedData);

            // 更新内存缓存
            _memoryCache[key] = value;
        }

        public async Task<bool> RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or whitespace", nameof(key));

            // 从持久化存储删除
            var success = await _deleter.DeleteAsync(key);

            // 从内存缓存删除
            _memoryCache.Remove(key);

            return success;
        }

        public async Task ClearAsync()
        {
            // 清空持久化存储
            await _deleter.ClearAsync();

            // 清空内存缓存
            _memoryCache.Clear();
        }

        public async Task<IEnumerable<string>> GetAllKeysAsync()
        {
            // 获取所有持久化存储的键
            //var persistentKeys = await _reader.GetAllKeysAsync();

            // 合并内存缓存的键
            var memoryKeys = _memoryCache.Keys;

            // 返回合并后的唯一键集合
            return memoryKeys; // persistentKeys.Union(memoryKeys).Distinct();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // 释放托管资源
                (_writer as IDisposable)?.Dispose();
                (_reader as IDisposable)?.Dispose();
                (_deleter as IDisposable)?.Dispose();

                _memoryCache.Clear();
            }

            _disposed = true;
        }

        ~JDataStore()
        {
            Dispose(false);
        }
    }
}