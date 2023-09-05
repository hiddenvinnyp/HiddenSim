using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="StaticData/WindowData", fileName = "WindowData")]
public class WindowStaticData : ScriptableObject
{
    public List<WindowConfig> Configs;
}