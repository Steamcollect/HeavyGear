using UnityEngine;

public class StageUiManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject prefabStageUi;
    [SerializeField] private StageUI stageUiFactory;
    [Space(10)]
    [SerializeField] private RSO_StageData rsoStageData;

    private bool isInited;

    private void OnStageUiContentChanged()
    {
        UpdateUI();
    }

    private void InitContent()
    {
        
        stageUiFactory.SetStageUI(rsoStageData.Value.stageNextFactoryState, false);
    }

    private void OnEnable() => rsoStageData.OnChanged += OnStageUiContentChanged;

    private void OnDisable() => rsoStageData.OnChanged -= OnStageUiContentChanged;
    
    private void UpdateUI()
    {
        if (!isInited)
        {
            isInited = true;
            InitContent();
        }
        else
        {
            stageUiFactory.UpdateStageUI(rsoStageData.Value.stageNextFactoryState.Item2);
        }
    }
}