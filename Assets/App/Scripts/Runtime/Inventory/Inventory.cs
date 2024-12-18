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
    [SerializeField] private RSE_UpdateMachineInventory rseUpdateMachineInventory;

    public List<InventoryMachineData> content;

    public List<InventoryMachineData> Content
    {
        get => content;
        set
        {
            content = SortMachinesByRarity(value);
            rseUpdateMachineInventory.Call(content);
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
        Content = content;
        //Content = rsoContentSaved.Value.inventoryMachineData;
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
    }

    private List<InventoryMachineData> SortMachinesByRarity(List<InventoryMachineData> machines)
    {
        return machines.OrderByDescending(x => x.machine.machineRarity).ToList();
    }

    void LevelUp(InventoryMachineData slot)
    {
        slot.amount = 1;
        slot.machineLevel++;
        // Upgrade statistics
    }
}