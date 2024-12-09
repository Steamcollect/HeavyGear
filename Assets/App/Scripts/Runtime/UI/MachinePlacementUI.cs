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
    [SerializeField] Image visualImage;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_SelectMachineToPlace rseSelectMachineToPlace;

    public void Setup(SSO_MachinePlacementData machine)
    {
        machineData = machine;

        nameTxt.text = machineData.machineName;
        descriptionTxt.text = machineData.machineDescription;
        visualImage.sprite = machineData.machineVisual;
    }

    public void SelectButton()
    {
        rseSelectMachineToPlace.Call(machineData);
    }
}