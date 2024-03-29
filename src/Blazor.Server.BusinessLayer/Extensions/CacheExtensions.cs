﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Blazor.Server.BusinessLayer.Extensions
{
    public static class CacheExtensions
    {
        public static async Task<T> GetOrCreateAsync<T>(this IMemoryCache cache, object key,
            Func<Task<T>> factory, MemoryCacheEntryOptions cacheEntryOptions)
        {
            return await cache.GetOrCreateAsync(key, async entry =>
            {
                entry.SetOptions(cacheEntryOptions);
                return await factory();
            });
        }
    }
}
