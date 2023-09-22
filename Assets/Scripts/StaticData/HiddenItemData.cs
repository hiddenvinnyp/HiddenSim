using System;
using UnityEngine;

[Serializable]
public class HiddenItemData
{
    public string Id;
    public HiddenItem Prefs;

    public HiddenItemData(string id, HiddenItem prefs)
    {
        Id = id;
        Prefs = prefs;
    }
}
