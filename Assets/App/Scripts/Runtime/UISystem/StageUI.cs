using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Button button;
    [SerializeField] private GameObject checkImage;
    [SerializeField] private TMP_Text tmpText;

    public void SetStageUI(Tuple<string,bool> stageData, bool completed)
    {
        tmpText.text = stageData.Item1;
        if (completed) button.gameObject.SetActive(false);
        else
        {
            checkImage.SetActive(false);
            button.interactable = stageData.Item2;
        }
    }

    public void UpdateStageUI(bool buttonEnabled)
    {
        button.interactable = buttonEnabled;
    }
    
}