using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IAssets : IService
{
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 point);
    Task<T> Load<T>(AssetReference assetReference) where T : class;
    void CleanUp();
}