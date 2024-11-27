using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_BigNumberData", menuName = "ScriptableObject/SSO_BigNumberData")]
public class SSO_BigNumberData : ScriptableObject
{
    [TableList]
    public List<BigNumberDigits> bigNumberDigits;
}