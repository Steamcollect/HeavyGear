using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachinePlacementPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject panel;
    [SerializeField] Transform content;
    ContentSizeFitter contentSizeFitter;
    [SerializeField] MachinePlacementUI machinePlacementUIPrefab;
    List<MachinePlacementUI> machinesUI = new List<MachinePlacementUI>();

    [Header("Input")]
    [SerializeField] RSE_UpdateMachineInventory rseUpdateMachineInventory;
    [SerializeField] RSE_UpdateMachinePlacementPanelContent rseUpdateContent;

    //[Header("Input")]

    private void OnEnable()
    {
        rseUpdateContent.action += UpdateContent;
        rseUpdateMachineInventory.action += SetupContent;
    }
    private void OnDisable()
    {
        rseUpdateContent.action -= UpdateContent;
        rseUpdateMachineInventory.action -= SetupContent;
    }

    private void Awake()
    {
        contentSizeFitter = content.GetComponent<ContentSizeFitter>();
    }

    void SetupContent(List<InventoryMachineData> machines)
    {
        foreach (InventoryMachineData machine in machines)
        {
            MachinePlacementUI current = Instantiate(machinePlacementUIPrefab, content);
            current.Setup(machine);
            machinesUI.Add(current);
        }
    }

    void UpdateContent(MachineType machineType)
    {
        foreach (var machine in machinesUI)
        {
            if(machine.machineData.machineType == machineType) machine.gameObject.SetActive(true);
            else machine.gameObject.SetActive(false);
        }
    }
}
