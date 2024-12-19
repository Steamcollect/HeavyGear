using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class MachinePlacementManager : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] UnityEvent openMachinePlacementPanel;
    [SerializeField] UnityEvent closeMachinePlacementPanel;
    [SerializeField] RSE_UpdateMachinePlacementPanelContent rseUpdateMachinePlacementPanelContent;

    [Header("Output")]
    [SerializeField] RSE_AddNewMachine rseAddNewMachine;
    [SerializeField] RSE_SelectMachineToPlace rseSelecteMachineToPlace;

    MachineSlotSettings placementSettings;

    private void OnEnable()
    {
        rseAddNewMachine.action += OnItemPlacement;
        rseSelecteMachineToPlace.action += PlaceItem;
    }
    private void OnDisable()
    {
        rseAddNewMachine.action -= OnItemPlacement;
        rseSelecteMachineToPlace.action -= PlaceItem;
    }

    void OnItemPlacement(MachineSlotSettings settings)
    {
        placementSettings = settings;
        rseUpdateMachinePlacementPanelContent.Call(settings.machineType);
        openMachinePlacementPanel.Invoke();
    }

    void PlaceItem(SSO_MachinePlacementData machineSelected)
    {
        InteractiveMachineTemplate machine = Instantiate(machineSelected.machinePrefab, placementSettings.slot.transform);
        machine.transform.position = placementSettings.slot.transform.position;
        machine.transform.position = machine.transform.position + new Vector3(0, -0.25f);
        machine.transform.rotation = placementSettings.slot.transform.rotation;

        machine.SetupParentRequirement(placementSettings.clickable);
        machine.SetupChildRequirement(placementSettings);

        placementSettings.slot.UpdateCurrentMachine(machine);

        placementSettings = null;

        closeMachinePlacementPanel.Invoke();
    }
}