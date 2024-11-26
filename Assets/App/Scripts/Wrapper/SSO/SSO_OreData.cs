using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_OreData", menuName = "ScriptableObject/SSO_OreData")]
public class SSO_OreData : ScriptableObject
{
    [TableList]
    public List<OreData> oreData;
}