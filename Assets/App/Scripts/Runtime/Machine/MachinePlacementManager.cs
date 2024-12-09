using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MachinePlacementManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] List<SSO_MachinePlacementData> machines = new List<SSO_MachinePlacementData>();

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_OpenMachinePlacementPanel rseOpenMachinePlacementPanel;
    [SerializeField] RSE_CloseMachinePlacementPanel rseCloseMachinePlacementPanel;
    [SerializeField] RSE_SetupMachinePlacementPanelContent rseSetupMachinePlacementPanelContent;
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

    private void Start()
    {
        rseSetupMachinePlacementPanelContent.Call(machines);
    }

    void OnItemPlacement(MachineSlotSettings settings)
    {
        placementSettings = settings;
        rseUpdateMachinePlacementPanelContent.Call(settings.machineType);
        rseOpenMachinePlacementPanel.Call();
    }

    void PlaceItem(SSO_MachinePlacementData machineSelected)
    {
        InteractiveMachineTemplate machine = Instantiate(machineSelected.machinePrefab, placementSettings.slot.transform);
        machine.transform.position = placementSettings.slot.transform.position;
        machine.transform.rotation = placementSettings.slot.transform.rotation;

        machine.SetupParentRequirement(placementSettings.clickable);
        machine.SetupChildRequirement(placementSettings);

        placementSettings.slot.UpdateCurrentMachine(machine);

        placementSettings = null;

        rseCloseMachinePlacementPanel.Call();
    }
}