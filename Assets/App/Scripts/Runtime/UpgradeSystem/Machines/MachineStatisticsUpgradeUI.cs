using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineStatisticsUpgradeUI : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] MachineStatisticsUpgradeButton[] upgradesButtons;
    private List<int> upgradesLevels = new();
    
    [Space(10)]
    [SerializeField] RSO_ContentSaved rsoContentSaved;
    

    private void Start()
    {
        StartCoroutine(LateStart());
    }
    
    
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);
        if(rsoContentSaved.Value.machineButtonsUpgrades.Count == 0)
        {
            for (int i = 0; i < upgradesButtons.Length; i++) upgradesLevels.Add(1);
            rsoContentSaved.Value.machineButtonsUpgrades = upgradesLevels;
        }
        else
        {
            upgradesLevels = rsoContentSaved.Value.machineButtonsUpgrades;
        }
        
        for(int i = 0; i < upgradesButtons.Length; i++)
        {
            upgradesButtons[i].Setup(upgradesLevels[i], i);
            upgradesButtons[i].OnLevelChange += OnButtonValueChanged;
        }
    }

    public void OnButtonValueChanged(int level, int index)
    {
        upgradesLevels[index] = level;
    }
}