using UnityEngine;

public interface IAssetProvider
{
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 point);
}