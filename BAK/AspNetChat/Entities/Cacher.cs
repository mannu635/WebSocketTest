using System;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;

namespace AspNetChat
{
  public interface ICasher
  {
    object Add(string key, object value);
    void Remove(string key);
    object Get(string key);
    void Clear();
    bool Contains(string key);

  }
  public class Cacher : ICasher
  {
    private static readonly object _syncObject = new object();
    private MemoryCache _cache { get; } = MemoryCache.Default;
    private static int _cachetimer = 6;
    private CacheItemPolicy _defaultPolicy { get; } = new CacheItemPolicy();

    public void Clear()
    {
      var keys = _cache.Select(i => i.Key).ToList();
      keys.ForEach(k => _cache.Remove(k));
    }

    public object Get(string key)
    {
      return _cache.Get(key);
    }

    public void Remove(string key)
    {
      lock (_syncObject)
      {
        if (_cache.Contains(key)) _cache.Remove(key);
      }
    }
    public object Add(string key, object value)
    {
      lock (_syncObject)
      {
        return _cache.Add(key, value, DateTimeOffset.Now.AddHours(_cachetimer));
      }
    }
    public bool Contains(string key)
    {
      return _cache.Contains(key);
    }

  }
}
