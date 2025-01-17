using System;
using System.Threading.Tasks;
using BigFloatNumerics;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class FactoryStages : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField] private SSO_FactoryStageData ssoFactoryStageData;
    
    [Title("References")]
    [SerializeField] private RSO_StageData rsoStageData;
    [SerializeField] private RSO_Coins rsoCoins;
    [SerializeField] private RSO_ContentSaved rsoContentSaved;
    
    [Header("Input")]
    [SerializeField] private RSE_NextFactoryLoad rseNextFactoryLoad;
    [SerializeField] private RSE_NextStageLoad rseNextStageLoad;
    [Header("Output")]
    [SerializeField] private RSE_RemoveCoin rseRemoveCoin;
    [SerializeField] private RSE_LoadNewScene rseLoadNewScene;
    
    private bool currentStageComplete;

    private string targetStage;
    
    private void OnEnable()
    {
        rsoCoins.OnChanged += CheckCurrentStageIsComplete;
        rseNextFactoryLoad.action += OnNextFactoryLoad;
        rseNextStageLoad.action += OnNextStageLoad;
    }

    private void OnDisable()
    {
        rsoCoins.OnChanged -= CheckCurrentStageIsComplete;
        rseNextFactoryLoad.action -= OnNextFactoryLoad;
        rseNextStageLoad.action -= OnNextStageLoad;
    }

    private async void Start()
    {
        await Task.Delay(10);
        InitFactoryStage();
        CheckCurrentStageIsComplete();
    }

    private void InitFactoryStage()
    {
        targetStage = ssoFactoryStageData.nextFactoryStage.ToStringData();
        if (targetStage == "") Debug.LogWarning("No target stage found");
        rsoStageData.Value = new StageData
        {
            stageNextFactoryState = new Tuple<string, bool>(ssoFactoryStageData.nextFactoryStage.ToStringData(),false),
            currentStage = rsoContentSaved.Value.currentStageFactory,
            nextStageName = ssoFactoryStageData.nextFactorySceneName
        };
    }

    private void CheckCurrentStageIsComplete()
    {
        if (rsoCoins.Value == null || string.IsNullOrEmpty(targetStage)) return;
        
        if (currentStageComplete)
        {
            if (!DoesValueReachedGoalValue(rsoCoins.Value, new BigNumber(targetStage)))
            {
                currentStageComplete = false;
                rsoStageData.Value.stageNextFactoryState = new Tuple<string, bool>(rsoStageData.Value.stageNextFactoryState.Item1, false);
                rsoStageData.Value = rsoStageData.Value;
            }
        }
        else
        {
            if (DoesValueReachedGoalValue(rsoCoins.Value, new BigNumber(targetStage)))
            {
                currentStageComplete = true;
                rsoStageData.Value.stageNextFactoryState = new Tuple<string, bool>(rsoStageData.Value.stageNextFactoryState.Item1, true);
                rsoStageData.Value = rsoStageData.Value;
            }
        }
    }

    private bool DoesValueReachedGoalValue(BigNumber value, BigNumber aimValue)
    {
        return value.CompareTo(aimValue) is 0 or 1;
    }

    private void OnNextStageLoad()
    {
        rsoStageData.Value.currentStage = Mathf.Clamp(rsoStageData.Value.currentStage + 1, 0, ssoFactoryStageData.rebirthStage.Count);
        rseRemoveCoin.Call(new BigNumber(rsoCoins.Value));
        rseLoadNewScene.Call(rsoContentSaved.Value.currentFactory);
    }

    private void OnNextFactoryLoad()
    {
        rsoStageData.Value.currentStage = 0;
        rseRemoveCoin.Call(new BigNumber(rsoCoins.Value));
        rseLoadNewScene.Call(rsoStageData.Value.nextStageName);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus) return;
        rsoContentSaved.Value.currentStageFactory = rsoStageData.Value.currentStage;
    }

    private void OnDestroy()
    {
        rsoContentSaved.Value.currentStageFactory = rsoStageData.Value.currentStage;
        rsoStageData.Value = null;
    }
}