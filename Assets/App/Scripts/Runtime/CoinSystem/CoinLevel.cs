using System;
using BigFloatNumerics;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinLevel : MonoBehaviour
{

    [Title("References")]
    [SerializeField] private SSO_CoinLevel ssoCoinLevel;
    [Space(10)]
    [SerializeField] private RSO_Coins rsoCoins;
    [SerializeField] private RSO_ContentSaved rsoContentSaved;
    
    private bool aimReached = false;
    
    [Title("Output")]
    [SerializeField] private RSE_LoadNewScene rseLoadNewScene;
    [SerializeField] private RSE_NextFactoryReached rseNextFactoryReached;
    [SerializeField] private RSE_NextLevelReached rseNextLevelReached;

    public void OnEnable() => rsoCoins.OnChanged += CheckLevelReached;
    public void OnDisable() => rsoCoins.OnChanged -= CheckLevelReached;

    private void CheckLevelReached()
    {
        if (aimReached) return;
        if (rsoCoins.Value.CompareTo(0) == 0) return;
        
        if (rsoContentSaved.Value.currentCoinLevel >= ssoCoinLevel.rebirthLevels.Count)
        {
            CompareAimValueReached(ssoCoinLevel.nextFactoryLevel,()=>
            {
                Debug.Log("Next Factory reached");
                aimReached = true;
                rseNextFactoryReached.Call();
                rsoContentSaved.Value.currentCoinLevel = 0;
                rsoCoins.Value = new(0);
                rseLoadNewScene.Call(ssoCoinLevel.nextFactorySceneName);
            });
        }
        else
        {
            CompareAimValueReached(ssoCoinLevel.rebirthLevels[rsoContentSaved.Value.currentCoinLevel], () =>
            {
                Debug.Log("Level reached");
                aimReached = true;
                rsoContentSaved.Value.currentCoinLevel = Mathf.Clamp(rsoContentSaved.Value.currentCoinLevel + 1,0,ssoCoinLevel.rebirthLevels.Count);
                rseNextLevelReached.Call();
                rsoCoins.Value = new(0);
                rseLoadNewScene.Call(SceneManager.GetActiveScene().name);
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