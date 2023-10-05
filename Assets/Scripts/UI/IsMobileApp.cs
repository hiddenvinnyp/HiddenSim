using UnityEngine;

public class IsMobileApp : MonoBehaviour
{
    void Start()
    {
        if (Application.isMobilePlatform)
            gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
