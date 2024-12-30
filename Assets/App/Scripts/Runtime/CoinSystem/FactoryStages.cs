using System;
using BigFloatNumerics;
using Sirenix.OdinInspector;
using UnityEngine;

public class FactoryStages : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField] private SSO_FactoryStageData ssoFactoryStageData;
    
    [Title("References")]
    [SerializeField] private RSO_CurrentStages rsoCurrentStages;
    [SerializeField] private RSO_Coins rsoCoins;
    [SerializeField] private RSO_ContentSaved rsoContentSaved;
    
    private bool currentGoalReached;
    
    [Title("Output")]
    [SerializeField] private RSE_LastFactoryReached rseLastFactoryReached;
    [SerializeField] private RSE_NextFactoryReached rseNextFactoryReached;
    [SerializeField] private RSE_NextLevelReached rseNextLevelReached;

    public void OnEnable()
    {
        rsoCoins.OnChanged += CheckStageReached;
        rsoCurrentStages.Value = new StageData
        {
            factoryStageData = ssoFactoryStageData,
            currentStage = rsoContentSaved.Value.currentStageFactory,
            lastStageReached = rsoContentSaved.Value.lastStageReached
        };
    }

    public void OnDisable()
    {
        rsoCoins.OnChanged -= CheckStageReached;
        rsoCurrentStages.Value = null;
    }

    public void Start()
    {
        CheckStateStagesFromData();
    }

    private void CheckStateStagesFromData()
    {
        if (DoesCurrentStageReached()) currentGoalReached = true;
    }
    
    private void CheckStageReached()
    {
        if (currentGoalReached) return;
        if (rsoCoins.Value.CompareTo(0) == 0) return;
        
        if (rsoCurrentStages.Value.currentStage >= ssoFactoryStageData.rebirthStage.Count)
        {
            
            CompareGoalValueReached(ssoFactoryStageData.nextFactoryStage,()=>
            {
                if (ssoFactoryStageData.lastLevel)
                {
                    currentGoalReached = true;
                    rseLastFactoryReached.Call();
                    return;
                }
                
                Debug.Log("Next Factory reached");
                currentGoalReached = true;
                rsoCurrentStages.Value.lastStageReached = true;
                rseNextFactoryReached.Call();
                
            });
        }
        else
        {
            CompareGoalValueReached(ssoFactoryStageData.rebirthStage[rsoCurrentStages.Value.currentStage], () =>
            {
                Debug.Log("Level reached");
                currentGoalReached = true;
                rsoCurrentStages.Value.currentStage = Mathf.Clamp(rsoCurrentStages.Value.currentStage + 1,0,ssoFactoryStageData.rebirthStage.Count);
                rseNextLevelReached.Call();
            });
        }
    }


    private void CompareGoalValueReached(BigNumber aimValue,Action callback = null)
    {
        int valueCompare = rsoCoins.Value.CompareTo(aimValue);
        if (valueCompare is 1 or 0)
        {
            callback?.Invoke();
        }
    }

    private bool CompareGoalValueReached(BigNumber aimValue, BigNumber currentValue)
    {
        int valueCompare = currentValue.CompareTo(aimValue);
        return valueCompare is 1 or 0;
    }

    private bool DoesCurrentStageReached()
    {
        bool reached;
        BigNumber currentValue = new BigNumber(rsoContentSaved.Value.coinAmount);
        reached = CompareGoalValueReached(rsoContentSaved.Value.lastStageReached ?
            ssoFactoryStageData.nextFactoryStage : ssoFactoryStageData.rebirthStage[rsoContentSaved.Value.currentStageFactory], currentValue);
        return reached;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus) return;
        rsoContentSaved.Value.lastStageReached = rsoCurrentStages.Value.lastStageReached;
        rsoContentSaved.Value.currentStageFactory = rsoCurrentStages.Value.currentStage;
    }
}