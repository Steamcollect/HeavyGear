using BT.Save;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("References")]
    // RSO
    [SerializeField] private RSO_ContentSaved rsoContentSaved;
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] private RSE_AddMachineToInventory rseAddSlot;

    [Header("Output")]
    [SerializeField] private RSO_MachinesListUI rsoMachinesListUI;

    private List<InventoryMachineData> content = new List<InventoryMachineData>();

    private List<InventoryMachineData> Content
    {
        get => content;
        set
        {
            content = value;

            rsoMachinesListUI.Value = value;
        }
    }

    private void OnEnable()
    {
        rseAddSlot.action += AddMachine;
    }
    private void OnDisable()
    {
        rseAddSlot.action -= AddMachine;
    }

    private void Start()
    {
        Content = rsoContentSaved.Value.inventoryMachineData;
    }

    void AddMachine(SSO_MachinePlacementData machine)
    {
        InventoryMachineData slot = Content.Find(x => x.machine == machine);
        if (slot != null)
        {
            slot.amount++;

            if(slot.amount >= slot.maxAmount) LevelUp(slot);
        }
        else
        {
            Content.Add(new InventoryMachineData { machine = machine, amount = 1, machineLevel = 1 });
        }

        SortMachinesByRarity();
    }

    private void SortMachinesByRarity()
    {
        Content = Content.OrderByDescending(x => x.machine.machineRarity).ToList();
    }

    void LevelUp(InventoryMachineData slot)
    {
        slot.amount = 1;
        slot.machineLevel++;
        // Upgrade statistics
    }
}