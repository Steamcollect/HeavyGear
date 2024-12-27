using Sirenix.OdinInspector;
using UnityEngine;

public abstract class SSO_ContentCrateData : ScriptableObject
{
    [Title("Settings")]
    [TableColumnWidth(64, Resizable = true)] 
    [PreviewField(64, Alignment = ObjectFieldAlignment.Center)]
    public Sprite icon;
    [Space(10)]
    public new string name;
    public abstract void LinkContextContentToData();
}