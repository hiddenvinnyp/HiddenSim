using System;
using UnityEngine;

[Serializable]
public class HiddenItemData
{
    public string Id;
    public string Name;
    public string Hint;
    public Sprite Icon;

    public HiddenItemData(string id, string name, string hint, Sprite icon)
    {
        Id = id;
        Name = name;
        Hint = hint;
        Icon = icon;
    }
}
