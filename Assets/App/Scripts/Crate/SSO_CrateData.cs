using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_CrateData", menuName = "ScriptableObject/SSO_CrateData")]
public class SSO_CrateData : ScriptableObject
{
    [Title("Settings")]
    [TableList] public List<Content> contentCrate;
}

[Serializable]
public class Content
{
    public string data = "";
    [MinValue(0.001f)] public float weight = 0.001f;
}