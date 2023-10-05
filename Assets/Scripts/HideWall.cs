using UnityEngine;

public class HideWall : MonoBehaviour
{
    [SerializeField] private GameObject _solid;
    [SerializeField] private GameObject _transparent;

    void Start()
    {
        MakeSolid();
    }

    public void MakeSolid()
    {
        _solid.SetActive(true);
        _transparent.SetActive(false);
    }

    public void MakeTransparent()
    {
        _solid.SetActive(false);
        _transparent.SetActive(true);
    }
}
