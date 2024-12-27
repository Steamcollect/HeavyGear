using System;
using BigFloatNumerics;
using Sirenix.OdinInspector;
using UnityEngine;
public class CoinLevel : MonoBehaviour
{

    [Title("References")]
    [SerializeField] private SSO_CoinLevel ssoCoinLevel;
    [Space(10)]
    [SerializeField] private RSO_Coins rsoCoins;
    [SerializeField] private RSO_ContentSaved rsoContentSaved;
    
    private bool aimReached = false;
    
    [Title("Output")]
    [SerializeField] private RSE_NextFactoryReached rseNextFactoryReached;
    [SerializeField] private RSE_NextLevelReached rseNextLevelReached;

    public void OnEnable() => rsoCoins.OnChanged += CheckLevelReached;
    public void OnDisable() => rsoCoins.OnChanged -= CheckLevelReached;

    private void CheckLevelReached()
    {
        if (aimReached) return;
        if (rsoCoins.Value.CompareTo(0) == 0) return;
        
        if (rsoContentSaved.Value.currentCoinLevel >= ssoCoinLevel.rebirthLevels.Count - 1)
        {
            CompareAimValueReached(ssoCoinLevel.nextFactoryLevel,()=>
            {
                Debug.Log("Next Factory reached");
                aimReached = true;
                rseNextFactoryReached.Call();
            });
        }
        else
        {
            CompareAimValueReached(ssoCoinLevel.rebirthLevels[rsoContentSaved.Value.currentCoinLevel], () =>
            {
                Debug.Log("Level reached");
                aimReached = true;
                rsoContentSaved.Value.currentCoinLevel = Mathf.Min(ssoCoinLevel.rebirthLevels.Count - 1,
                    rsoContentSaved.Value.currentCoinLevel + 1);
                rseNextLevelReached.Call();
            });
        }
    }


    private void CompareAimValueReached(BigNumber aimValue, Action callback)
    {
        int valueCompare = rsoCoins.Value.CompareTo(aimValue);
        if (valueCompare is 1 or 0)
        {
            callback?.Invoke();
        }
    }
}