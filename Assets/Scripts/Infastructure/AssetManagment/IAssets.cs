using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IAssets : IService
{
    Task<GameObject> Instantiate(string path);
    Task<GameObject> Instantiate(string path, Vector3 point);
    Task<T> Load<T>(AssetReference assetReference) where T : class;
    void CleanUp();
    Task<T> Load<T>(string address) where T : class;
    void Initialize();
}