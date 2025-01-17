using BigFloatNumerics;
using JetBrains.Annotations;
using UnityEngine;

public class StageUiManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject prefabStageUi;
    [SerializeField] private StageUI stageUiFactory;
    [Space(10)]
    [SerializeField] private RSO_StageData rsoStageData;

    [Header("Input")]
    [SerializeField] private RSE_NextFactoryLoad rseNextFactoryLoad;
    
    private bool isInited;
    
    private void OnStageUiContentChanged()
    {
        UpdateUI();
    }

    private void InitContent()
    {
        if (rsoStageData.Value == null) return;
        {
            isInited = true;
            stageUiFactory.SetStageUI(rsoStageData.Value.stageNextFactoryState, false);
        }
        
    }

    private void OnEnable()
    {
        rsoStageData.OnChanged += OnStageUiContentChanged;
        rseNextFactoryLoad.action += ResetUi;
    }

    private void OnDisable()
    {
        rsoStageData.OnChanged -= OnStageUiContentChanged;
        rseNextFactoryLoad.action -= ResetUi;
    }

    private void UpdateUI()
    {
        if (!isInited)
        {
            InitContent();
        }
        else
        {
            if (rsoStageData.Value == null) return; 
            stageUiFactory.UpdateStageUI(rsoStageData.Value.stageNextFactoryState.Item2);
        }
    }

    private void ResetUi()
    {
        isInited = false;
    }
}