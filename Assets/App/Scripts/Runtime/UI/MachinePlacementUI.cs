using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MachinePlacementUI : MonoBehaviour
{
    [Header("Settings")]
    [HideInInspector] public SSO_MachinePlacementData machineData;

    [Header("References")]
    [SerializeField] TMP_Text nameTxt;
    [SerializeField] TMP_Text descriptionTxt;
    [SerializeField] Image machineVisual;
    [SerializeField] Image backgroud;
    [SerializeField] Image border;
    [Space(10)]
    [SerializeField] private SSO_CardVisual common;
    [SerializeField] private SSO_CardVisual rare;
    [SerializeField] private SSO_CardVisual legendary;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_SelectMachineToPlace rseSelectMachineToPlace;

    public void Setup(InventoryMachineData machine)
    {
        machineData = machine.machine;

        nameTxt.text = machineData.machineName;
        descriptionTxt.text = machineData.machineDescription;
        machineVisual.sprite = machineData.machineVisual;

        switch (machineData.machineRarity)
        {
            case MachineRarity.Common:
                SetCardVisual(common);
                break;
            case MachineRarity.Rare:
                SetCardVisual(rare);
                break;
            case MachineRarity.Legendary:
                SetCardVisual(legendary);
                break;
            default:
                SetCardVisual(common);
                break;
        }
    }

    private void SetCardVisual(SSO_CardVisual cardVisual)
    {
        backgroud.color = cardVisual.firstColor;
        border.color = cardVisual.firstColor;
    }

    public void SelectButton()
    {
        rseSelectMachineToPlace.Call(machineData);
    }
}