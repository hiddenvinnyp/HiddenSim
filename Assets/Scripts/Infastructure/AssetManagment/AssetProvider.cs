using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetProvider : IAssets
{
    private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new Dictionary<string, AsyncOperationHandle>();
    private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

    public void Initialize()
    {
        Addressables.InitializeAsync();
    }

    public Task<GameObject> Instantiate(string path) =>
        Addressables.InstantiateAsync(path).Task;

    public Task<GameObject> Instantiate(string path, Vector3 point) =>
        Addressables.InstantiateAsync(path, point, Quaternion.identity).Task;

    public async Task<T> Load<T>(AssetReference assetReference) where T : class
    {
        if (_completedCache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
            return completedHandle.Result as T;

        return await RunWithCacheOnComplete(assetReference.AssetGUID, Addressables.LoadAssetAsync<T>(assetReference));
    }

    public async Task<T> Load<T>(string address) where T : class
    {
        if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
            return completedHandle.Result as T;

        return await RunWithCacheOnComplete(address, Addressables.LoadAssetAsync<T>(address));
    }

    public void CleanUp()
    {
        foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            foreach (AsyncOperationHandle handle in resourceHandles)
                Addressables.Release(handle);

        _completedCache.Clear();
        _handles.Clear();
    }

    private async Task<T> RunWithCacheOnComplete<T>(string cacheKey, AsyncOperationHandle<T> handle) where T : class
    {
        handle.Completed += completeHandle => _completedCache[cacheKey] = completeHandle;

        AddHandle(cacheKey, handle);

        return await handle.Task;
    }

    private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
    {
        if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
        {
            resourceHandles = new List<AsyncOperationHandle>();
            _handles[key] = resourceHandles;
        }
        resourceHandles.Add(handle);
    }
}