using UnityEngine;
using static UnityEditor.Progress;

public class HexagonLock : MonoBehaviour
{
    [SerializeField] private Material _lockMaterial;
    [SerializeField] private bool _isLocked;

    private MeshRenderer[] items;
    private Material[] _defaultMaterials;

    private void Start()
    {
        items = gameObject.GetComponentsInChildren<MeshRenderer>();
        _defaultMaterials = new Material[items.Length];

        for (int i = 0; i < items.Length; i++)
        {
            _defaultMaterials[i] = items[i].material;
        }
    }

    public void Locked(bool isLocked)
    {
        _isLocked = isLocked;

        if (isLocked)
        {
            foreach (var item in items)
            {
                item.material = _lockMaterial;
            }
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].material = _defaultMaterials[i];
            }
        }
    }
}
