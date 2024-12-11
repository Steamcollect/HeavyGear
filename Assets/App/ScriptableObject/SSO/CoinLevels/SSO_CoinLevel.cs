using BigFloatNumerics;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SSO_CoinLevel", menuName = "ScriptableObject/SSO_CoinLevel")]
public class SSO_CoinLevel : ScriptableObject
{
    [SerializeField] string[] _rebirthLevels;
    [SerializeField] string _nextFactoryLevel;

    [HideInInspector] public List<BigNumber> rebirthLevels = new List<BigNumber>();
    [HideInInspector] public BigNumber nextFactoryLevel;

    private void OnValidate()
    {
        rebirthLevels.Clear();
        foreach (var item in _rebirthLevels)
        {
            rebirthLevels.Add(new BigNumber(item));
        }

        nextFactoryLevel = new BigNumber(_nextFactoryLevel);
    }
}