using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineStatisticsUpgradeUI : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] MachineStatisticsUpgradeButton[] upgradesButtons;
    Dictionary<int, int> upgrades = new Dictionary<int, int>();

    [Space(10)]
    // RSO
    [SerializeField] RSO_ContentSaved rsoContentSaved;
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);
        if(rsoContentSaved.Value.machineButtonsUpgrades.Count == 0)
        {
            rsoContentSaved.Value.machineButtonsUpgrades = upgrades;
        }

        for(int i = 0; i < upgradesButtons.Length; i++)
        {
            upgradesButtons[i].Setup(upgrades[i], i);
            upgradesButtons[i].OnLevelChange += OnButtonValueChanged;
        }
    }

    public void OnButtonValueChanged(int level, int index)
    {
        upgrades[index] = level;
    }
}