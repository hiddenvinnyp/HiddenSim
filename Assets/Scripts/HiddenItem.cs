using UnityEngine;

[CreateAssetMenu(fileName = "HiddenItem")]
public class HiddenItem : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;
    [SerializeField] private string _hint;
    public string Hint => _hint;
    [SerializeField] private Sprite _icon;
    public Sprite Icon => _icon;
}
