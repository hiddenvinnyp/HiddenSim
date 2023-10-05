using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private const string TutorialPath = "TutorialCompleted";
    [SerializeField] private GameObject[] _hints;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(TutorialPath)) return;

        _hints[0].SetActive(true);
    }

    public void TutorialCompleted()
    {
        PlayerPrefs.SetInt(TutorialPath, 0);
    }
}
