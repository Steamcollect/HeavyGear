using System;
using BigFloatNumerics;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class FactoryStages : MonoBehaviour
{

    [Title("References")]
    [SerializeField] private SSO_FactoryStageData ssoFactoryStageData;
    [Space(10)]
    [SerializeField] private RSO_Coins rsoCoins;
    [SerializeField] private RSO_ContentSaved rsoContentSaved;
    
    private bool allGoalsReached = false;
    
    [Title("Output")]
    [SerializeField] private RSE_LoadNewScene rseLoadNewScene;
    [SerializeField] private RSE_NextFactoryReached rseNextFactoryReached;
    [SerializeField] private RSE_NextLevelReached rseNextLevelReached;

    public void OnEnable() => rsoCoins.OnChanged += CheckStageReached;
    public void OnDisable() => rsoCoins.OnChanged -= CheckStageReached;

    private void CheckStageReached()
    {
        if (allGoalsReached) return;
        if (rsoCoins.Value.CompareTo(0) == 0) return;
        
        //Check if it claim all goals
        if (rsoContentSaved.Value.currentStageFactory >= ssoFactoryStageData.rebirthStage.Count)
        {
            CompareGoalValueReached(ssoFactoryStageData.nextFactoryStage,()=>
            {
                Debug.Log("Next Factory reached");
                allGoalsReached = true;
                rseNextFactoryReached.Call();
                rsoContentSaved.Value.currentStageFactory = 0;
                rsoCoins.Value = new(0);
                rseLoadNewScene.Call(ssoFactoryStageData.nextFactorySceneName);
            });
        }
        else
        {
            CompareGoalValueReached(ssoFactoryStageData.rebirthStage[rsoContentSaved.Value.currentStageFactory], () =>
            {
                Debug.Log("Level reached");
                allGoalsReached = true;
                rsoContentSaved.Value.currentStageFactory = Mathf.Clamp(rsoContentSaved.Value.currentStageFactory + 1,0,ssoFactoryStageData.rebirthStage.Count);
                rseNextLevelReached.Call();
                rsoCoins.Value = new(0);
                rseLoadNewScene.Call(SceneManager.GetActiveScene().name);
            });
        }
    }


    private void CompareGoalValueReached(BigNumber aimValue, Action callback)
    {
        int valueCompare = rsoCoins.Value.CompareTo(aimValue);
        if (valueCompare is 1 or 0)
        {
            callback?.Invoke();
        }
    }
}