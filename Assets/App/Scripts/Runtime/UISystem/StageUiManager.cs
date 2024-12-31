using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StageUiManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameObject prefabStageUi;
    [SerializeField] private StageUI stageUiFactory;
    [SerializeField] private GridLayoutGroup container;
    [Space(10)]
    [SerializeField] private RSO_StageData rsoStageData;

    private List<StageUI> stagesUI = new();

    private void InitContent()
    {
        for (int i = 0; i < rsoStageData.Value.stagesState.Length; i++)
        {
            GameObject stageUiObj = Instantiate(prefabStageUi, container.transform);
            StageUI stageUi = stageUiObj.GetComponent<StageUI>();
            stageUi.SetStageUI(rsoStageData.Value.stagesState[i], i < rsoStageData.Value.currentStage);
            stagesUI.Add(stageUi);
        }
        stageUiFactory.SetStageUI(rsoStageData.Value.stageNextFactoryState, false);
    }

    private void OnEnable() => rsoStageData.OnChanged += UpdateUI;

    private void OnDisable() => rsoStageData.OnChanged -= UpdateUI;

    private void UpdateUI()
    {
        print("pass");
        if (stagesUI.Count == 0 && rsoStageData != null) InitContent();
        if  (rsoStageData.Value == null) return;
        if (rsoStageData.Value.currentStage < rsoStageData.Value.stagesState.Length)
        {
            stagesUI[rsoStageData.Value.currentStage].UpdateStageUI(rsoStageData.Value.stagesState[rsoStageData.Value.currentStage].Item2);
        }
        else
        {
            stageUiFactory.UpdateStageUI(rsoStageData.Value.stageNextFactoryState.Item2);
        }
    }
    
}
