﻿using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace Volo.Abp.FeatureManagement
{
    public class FeatureManagementStore : IFeatureManagementStore, ITransientDependency
    {
        protected IDistributedCache<FeatureValueCacheItem> Cache { get; }
        protected IFeatureValueRepository FeatureValueRepository { get; }
        protected IGuidGenerator GuidGenerator { get; }

        public FeatureManagementStore(
            IFeatureValueRepository featureValueRepository,
            IGuidGenerator guidGenerator,
            IDistributedCache<FeatureValueCacheItem> cache)
        {
            FeatureValueRepository = featureValueRepository;
            GuidGenerator = guidGenerator;
            Cache = cache;
        }

        [UnitOfWork]
        public virtual async Task<string> GetOrNullAsync(string name, string providerName, string providerKey)
        {
            var cacheItem = await GetCacheItemAsync(name, providerName, providerKey);
            return cacheItem.Value;
        }

        [UnitOfWork]
        public virtual async Task SetAsync(string name, string value, string providerName, string providerKey)
        {
            var featureValue = await FeatureValueRepository.FindAsync(name, providerName, providerKey);
            if (featureValue == null)
            {
                featureValue = new FeatureValue(GuidGenerator.Create(), name, value, providerName, providerKey);
                await FeatureValueRepository.InsertAsync(featureValue);
            }
            else
            {
                featureValue.Value = value;
                await FeatureValueRepository.UpdateAsync(featureValue);
            }

            await Cache.SetAsync(CalculateCacheKey(name, providerName, providerKey), new FeatureValueCacheItem(featureValue?.Value), considerUow: true);
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(string name, string providerName, string providerKey)
        {
            var featureValues = await FeatureValueRepository.FindAllAsync(name, providerName, providerKey);
            foreach (var featureValue in featureValues)
            {
                await FeatureValueRepository.DeleteAsync(featureValue);
                await Cache.RemoveAsync(CalculateCacheKey(name, providerName, providerKey), considerUow: true);
            }
        }

        protected virtual async Task<FeatureValueCacheItem> GetCacheItemAsync(string name, string providerName, string providerKey)
        {
            var cacheKey = CalculateCacheKey(name, providerName, providerKey);
            var cacheItem = await Cache.GetAsync(cacheKey, considerUow: true);

            if (cacheItem != null)
            {
                return cacheItem;
            }

            var featureValue = await FeatureValueRepository.FindAsync(name, providerName, providerKey);

            cacheItem = new FeatureValueCacheItem(featureValue?.Value);

            await Cache.SetAsync(
                cacheKey,
                cacheItem,
                considerUow: true
            );

            return cacheItem;
        }

        protected virtual string CalculateCacheKey(string name, string providerName, string providerKey)
        {
            return FeatureValueCacheItem.CalculateCacheKey(name, providerName, providerKey);
        }
    }
}
