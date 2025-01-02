using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StageUiManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PanelSwapper panelSwapper;
    [SerializeField] private GameObject prefabStageUi;
    [SerializeField] private StageUI stageUiFactory;
    [SerializeField] private GridLayoutGroup container;
    [Space(10)]
    [SerializeField] private RSO_StageData rsoStageData;

    private List<StageUI> stagesUI = new();


    private void OnStageUiContentChanged()
    {
        if (rsoStageData.Value == null)
        {
            ClearUi();
            panelSwapper.SwapPanel();
        }
        else
        {
            UpdateUI();
        }
    }

    private void ClearUi()
    {
        for (int i = 0; i < stagesUI.Count; i++)
        {
            Destroy(stagesUI[i].gameObject);
        }
        stagesUI.Clear();
    }

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

    private void OnEnable() => rsoStageData.OnChanged += OnStageUiContentChanged;

    private void OnDisable() => rsoStageData.OnChanged -= OnStageUiContentChanged;
    
    private void UpdateUI()
    {
        if (stagesUI.Count == 0) InitContent();
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
